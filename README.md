English | [中文](README_zh.md)

# RhythmBase Tutorial

This project serves Rhythm Doctor level developers, aiming to provide a more systematic and intuitive level editing medium for developers.  
Thanks to the Rhythm Doctor fan community for their support of this project.
Welcome to the RhythmBase tutorial. This guide will help you get started with setting up and using RhythmBase in your project.

## Install NuGet Package

To install the RhythmBase NuGet package, follow these steps:

1. Open Visual Studio or use the command line tools.
2. In Visual Studio, go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.
3. In the console, enter the following command:

	```
	Install-Package RhythmBase -Version 1.2.0-rc2
	```

4. Wait for the installation to complete and ensure your project references the required NuGet package.
5. To install using the .NET CLI, use:

	```
	dotnet add package RhythmBase -version 1.2.0-rc2
	```

## Coding
### Creating a Level

A level is a collection of events.

```cs
using RhythmBase.RhythmDoctor.Components;

using RDLevel emptyLevel = [];
Console.WriteLine(emptyLevel); // "" Count = 0
```

You can also use the built-in template to create a level with basic events.  
This template is the same as the default level template created by the Rhythm Doctor Level Editor.

```cs
using RhythmBase.RhythmDoctor.Components;

using RDLevel defaultLevel = RDLevel.Default;
Console.WriteLine(defaultLevel); // "" Count = 3
```

### Reading and Writing

You can directly import and export files using file paths. The default read/write settings will be used.  
Exporting does not package the level as a `.rdzip` file.

```cs
using RhythmBase.RhythmDoctor.Components;

// Directly read a level file
using RDLevel rdlevel1 = RDLevel.FromFile(@"your\level.rdlevel");

// Read a level pack file
using RDLevel rdlevel2 = RDLevel.FromFile(@"your\level.rdzip");

// Read a compressed level pack
using RDLevel rdlevel3 = RDLevel.FromFile(@"your\level.zip");

// Write a level file
rdlevel1.SaveToFile(@"your\outLevel.rdlevel");
```

You can add custom read/write settings with `LevelReadOrWriteSettings` when reading or writing levels.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Settings;

// Create custom read/write settings
LevelReadOrWriteSettings settings = new()
{
	// Handling of inactive events
	InactiveEventsHandling = InactiveEventsHandling.Store,
	// Handling of unreadable events
	// Common when sprite events are not bound to sprite tracks, etc.
	UnreadableEventsHandling = UnreadableEventHandling.Store,
	// Enable indentation
	Indented = true,
};

using RDLevel rdlevel1 = RDLevel.FromFile(@"your\level.rdlevel", settings);
```

You can also generate a JSON object or JSON string for further operations.

```cs
using RhythmBase.RhythmDoctor.Components;

LevelReadOrWriteSettings settings = new();

JsonDocument jobject = rdlevel.ToJsonDocument();
string json = rdlevel.ToJsonString(settings);
Console.WriteLine(jobject);
Console.WriteLine(json);
```

`LevelReadOrWriteSettings` provides `BeforeReading`, `AfterReading`, `BeforeWriting`, and `AfterWriting` events, which are triggered before/after reading or writing a level.  
You can add listeners to these events for custom behaviors.

```cs
using RhythmBase.RhythmDoctor.Settings;

settings.AfterWriting += Settings_AfterReading;

// This will be triggered after writing is finished
void Settings_AfterReading(object? sender, EventArgs e)
{
	throw new NotImplementedException();
}

rdlevel.Write(@"your\outLevel.rdlevel", settings);
```

> When reading compressed level pack files, please use the `using` statement or actively call the `RDLevel.Dispose()` method to ensure that temporary files extracted during decompression are properly cleaned up.

### Finding and Retrieving Events

The `OrderedEventCollection` type is used to store collections of events, and `RDLevel` inherits from this type.

You can use extension methods designed for querying Rhythm Doctor level events to simplify your queries.  
For example, you can filter by event type and its base types, interfaces implemented by events, beat ranges, custom predicates, and more.  
Methods such as `AddRange()`, `RemoveRange()`, `OfEvent()`, `RemoveAll()`, `InRange` are provided.

It is recommended to use these optimized methods for better performance.

```cs
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;

// Find MoveRow events between measures 3 and 5, and in event rows 0 to 2
var list = rdlevel
	.OfEvent<MoveRow>()
	.InRange(new(3, 1), new(5, 1))// From Bar 3 to 5
	.Where(i => 0 <= i.Y && i.Y < 3);  // In event rows 0 to 2
```

`Row` and `Decoration` also inherit from `OrderedEventCollection`, so rows and decorations also support these extension methods.

```cs
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;

// Find the AddClassicBeat event in the decoration between beat (11,1) and (13,1)
var list = rdlevel.Decorations[0]
	.OfEvent<AddClassicBeat>()
	.InRange(
		new RDBeat(11, 1), // Start searching from bar 11, beat 1
		new RDBeat(13, 1)  // End searching at bar 13, beat 1
	);
```
### Creating a Beat

`RDBeat` is a struct that stores three pieces of information: `BeatOnly`, `BarBeat`, and `TimeSpan`.

You can create a `RDBeat` instance that is not associated with a level, but due to the lack of a level context, its functionality may be limited.  
You can check its `IsEmpty` property to determine whether the instance is valid.  
When not associated with a level, calling its `ToString()` method will display the information it contains and indicate any missing information.

```cs
using RhythmBase.RhythmDoctor.Components;

// Create a beat not associated with a level
RDBeat beat1 = new(11);
RDBeat beat2 = new(2, 3);
RDBeat beat3 = new(TimeSpan.FromSeconds(11.45));

Console.WriteLine(beat1); // [10,?,?]
Console.WriteLine(beat2); // [?,(2, 3),?]
Console.WriteLine(beat3); // [?,?,00:00:11.4500000]
```

You can create a beat associated with a level using a `BeatCalculator` instance or an `RDLevel` instance.

A `BeatCalculator` is created along with an `RDLevel` and can be accessed via `RDLevel.Calculator`.  
When associated with a level, calling its `ToString()` method will display the `BarBeat` property.  
Only when associated with a level can all three properties be linked, and the beat can participate in all operations. Otherwise, only the properties with data can be used in calculations.  
The beat properties of events and bookmarks within a level are all associated with the level, while events removed from the level will lose this association.

```cs
using RhythmBase.RhythmDoctor.Components;

// Create a beat associated with a level
RDBeat beat1 = rdlevel.BeatOf(11);
RDBeat beat2 = rdlevel.Calculator.BeatOf(2, 3);
RDBeat beat3 = beat1 - 10 + TimeSpan.FromSeconds(11.45);

Console.WriteLine(beat1); // [2,3]
Console.WriteLine(beat2); // [2,3]
Console.WriteLine(beat3); // [3,4.083334]
```

When performing operations between beats, if both are linked to a level, you must ensure they refer to the same level.  
You can call the `FromSameLevel()` or `FromSameLevelOrNull()` methods to check if they refer to the same level.  
You can call `WithoutLink()` to return a new beat instance detached from any level.

```cs
using RhythmBase.RhythmDoctor.Components;

RDBeat beat1 = rdlevel.BeatOf(1);
RDBeat beat2 = beat1.WithoutLink();

Console.WriteLine(beat1.FromSameLevel(beat2));	   // False
Console.WriteLine(beat1.FromSameLevelOrNull(beat2)); // True
```

`BeatCalculator` also provides methods for time conversion, allowing you to convert between different time units.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;

(float, float) barbeat = rdlevel.Calculator.TimeSpanToBarBeat(TimeSpan.FromSeconds(19.19)); // (4, 8.983334)
```

`RDLevel` provides a default beat associated with the instance, with a beat count of 1.

```cs
using RhythmBase.RhythmDoctor.Components;

RDBeat @default = RDLevel.DefaultBeat;
```

`RDRange` is a data type similar to `Range`, used to represent a range of beats.  
It is commonly used for querying events.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extension;

var result = rdlevel.InRange(new RDRange(rdlevel.DefaultBeat + 10, null));
```
### Extended Data Types

Types in RhythmBase that start with `RD` and contain names like `Point`, `Size`, `Rect`, or `RotatedRect` are data types related to planar geometry.

Types ending with `I` are integer types, where all data properties are of type `int`, such as `RDPointI.X`.  
Types ending with `N` are non-nullable types, where all data properties are non-nullable, such as `RDSizeN.Height`.  
Types ending with `E` are expression types, where all data properties are of type `RDExpression`, such as `RDRectE.Size`.  
The `Angle` property of `RotatedRect` is always a floating-point type and is not affected by the `I` naming convention.

`RDExpression` is used to store Rhythm Doctor expressions and attempts to parse and evaluate them (not fully implemented yet).  
It is created from a string and supports simple operations.  
The underlying implementation is string concatenation, so it is normal for nested parentheses to appear when performing multiple operations.

```cs
using RhythmBase.RhythmDoctor.Components;

RDExpression exp1 = new("i2+1");
RDExpression exp2 = new(30);
RDExpression exp3 = new("25.5");

RDExpression result = exp1 - exp2 * exp3;

Console.WriteLine(result.ExpressionValue); // i2+1-765
```
### Creating and Modifying Events

All events directly or indirectly implement the `IBaseEvent` interface and inherit from the `BaseEvent` abstract type.  
You can use these interfaces and abstract types as generic parameters for query extension methods to filter events.  
For example,  
`BaseRowAction` and `BaseDecorationAction` are track events and sprite events, respectively,  
while `IRoomEvent` represents events with multi-room properties.  
The beat parameter used when creating an event can be unbound from any level; when the event is added to a level, it will be associated with that level, and when removed, the association will be broken.  
If no beat parameter is provided, it defaults to the first beat of the level.  
When calling the event's `ToString()` method, it will return a string in the format of the event's beat, event type, and displayable data.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;

Comment comment = new() { Beat = new(12), Text = "My_comment." };
Console.WriteLine(comment); // [11,?,?] Comment My_comment.

rdlevel.Add(comment);
Console.WriteLine(comment); // [2,4] Comment My_comment.

rdlevel.Remove(comment);
Console.WriteLine(comment); // [11,?,?] Comment My_comment.
```

In particular, adding, modifying, or removing a `SetCrotchetsPerBar` event will update the timeline after this event, so you don't need to worry about changes affecting the order or arrangement of events; they will remain fixed at their absolute beat positions. The level will also attempt to add new `SetCrotchetsPerBar` events or remove adjacent events with the same `CrotchetsPerBar` property to maintain the stability of other segments.

Row and decoration events need to be added using `Add()` on the corresponding row or decoration, while removal can be done from the row, decoration, or level using the `Remove()` method.  
Repeated additions have no effect.  
The event types `Comment` and `TintRows` are not subject to this restriction.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;

using RDLevel rdlevel = RDLevel.Default;

MoveRow tr = new();
Console.WriteLine(rdlevel); // "" Count = 3

rdlevel.Add(tint); // "" Count = 3

rdlevel.Rows[0].Add(tr);
Console.WriteLine(rdlevel); // "" Count = 4

rdlevel.Remove(tr);
Console.WriteLine(rdlevel); // "" Count = 3
```

### Forward Events

If the required event type is not available in this assembly, you can implement your own by inheriting from `ForwardEvent`, `ForwardRowEvent`, or `ForwardDecorationEvent`.

```cs
using Newtonsoft.RhythmDoctor.Json.Linq;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Components;
  
// Create a MyEvent type  
//   Inherit from ForwardEvent  
public class MyEvent : ForwardEvent
{
	// Override property  
	public override Tabs Tab => Tabs.Actions;

	// All implemented properties need to be bound to and checked for null in the CustomEvent.Data field.  

	// Implement an RDPointE type property  
	public RDPointE? MyProperty
	{
		get
		{
			// Get the required content from the Data field and check for null  
			return ExtraData.TryGetValue("myProperty", out var jsonElement)
				? jsonElement.Deserialize<RDPointE>()
				: null;
		}
		set
		{
			// Save the content in the Data field  
			ExtraData["myProperty"] =
				value.HasValue ?
				JsonElement.Parse(
					JsonSerializer.Serialize(value, Utils.GetJsonSerializerOptions())
				) :
				default;
		}
	}

	// Initialize the type in the constructor  
	public MyEvent()
	{
		// Initialize the ActureType property.
		ActureType = nameof(MyEvent);
	}
}
```

After writing your type, it can be read and written like a normal event.  
Note that `Type` is still `EventType.ForwardEvent`, while `ActureType` is the custom type name.

```cs
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Components;

MyEvent myEvent = new();

myEvent.MyProperty = new(2, "i3+1");

rdlevel.Add(myEvent);

myEvent.Beat = new(8);

Console.WriteLine(myEvent.Type);		// ForwardEvent  
Console.WriteLine(myEvent.ActureType);  // MyEvent  
```

If an unknown event type is encountered when reading a level, it will also be read as the corresponding `ForwardEvent`, `ForwardRowEvent`, or `ForwardDecorationEvent` type event.

### Event Types and Enums

All events have the `EaseType` property, and you can use methods in `EventTypeUtils` to convert and obtain the corresponding type.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;

Console.WriteLine(EventType.Tint.ToType());											   // RhythmBase.Events.Tint
Console.WriteLine(EventTypeUtils.ToType("Tint"));										 // RhythmBase.Events.Tint
Console.WriteLine(EventTypeUtils.ToEnum(typeof(Tint)));								   // Tint
Console.WriteLine(EventTypeUtils.ToEnum<Tint>());										 // Tint
Console.WriteLine(string.Join(", ", EventTypeUtils.ToEnums(typeof(IBarBeginningEvent))));  // PlaySong,SetCrotchetsPerBar, SetHeartExplodeVolume
Console.WriteLine(string.Join(", ", EventTypeUtils.ToEnums<IBarBeginningEvent>()));		// PlaySong,SetCrotchetsPerBar, SetHeartExplodeVolume
```

`EventTypeUtils` also includes some event type classifications, such as:

```cs
using RhythmBase.RhythmDoctor.Utils;

Console.WriteLine(string.Join(",\n", EventTypeUtils.DecorationTypes));
// Comment,
// CustomDecorationEvent,
// Move,
// PlayAnimation,
// SetVisible,
// Tile,
// Tint

Console.WriteLine(string.Join(",\n", EventTypeUtils.EventTypeEnumsForCameraFX));
// MoveCamera,
// ShakeScreen,
// FlipScreen,
// PulseCamera

Console.WriteLine(string.Join(",\n", EventTypeUtils.EventTypeEnumsForUtility));
// Comment,
// TagAction,
// CallCustomMethod
```

### Rich Text and Dialogue Components

The rich text components are located in the `RhythmBase.Components.RichText` namespace. You can use the `+` operator to combine rich text with custom colors. Serialization and deserialization of rich text are also supported.

`RDLine<>` represents a complete rich text line.  
`RDPhrase<>` is a style fragment of rich text, which follows a single style.  
You can use a struct that implements `IRDRichStringStyle<>` to specify the style rules for the rich text. In the example below, `RDRichStringStyle` is a rich text style that only includes color.

All can be implicitly converted from a string. Note that the converted text will be plain rich text without any style.

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.Global.Components.RichText;

RDLine<RDRichStringStyle> line = RDLine<RDRichStringStyle>.Deserialize("Hel<color=#00FF00>lo");

Console.WriteLine(line.ToString()); // Hello
Console.WriteLine(line.Serialize()); // Hel<color=lime>lo</color>

line +=
	new RDPhrase<RDRichStringStyle>(" Rhythm")
	{
		Style = new()
		{
			Color = RDColor.Lime
		}
	};
	
line += " Doctor!";

Console.WriteLine(line.ToString()); // Hello Rhythm Doctor!
Console.WriteLine(line.Serialize()); // Hel<color=lime>lo Rhythm</color> Doctor!
```

Both `RDLine<>` and `RDPhrase<>` support index access and modification of their fragments.

```cs
using RhythmBase.Global.Components.RichText;

RDLine<RDRichStringStyle> line = RDLine<RDRichStringStyle>.Deserialize("Hel<color=#00FF00>lo Rhythm</color> Doctor!");

Console.WriteLine(line[6..].ToString()); // Rhythm Doctor!
Console.WriteLine(line[6..].Serialize()); // <color=lime>Rhythm</color> Doctor!

line[5] = " and Welcome to ";

Console.WriteLine(line.ToString()); // Hello and Welcome to Rhythm Doctor!
Console.WriteLine(line.Serialize()); // Hel<color=lime>lo</color> and Welcome to <color=lime>Rhythm</color> Doctor!

return;
```

This package also provides a complete set of dialogue components adapted to the Rhythm Doctor dialogue format, allowing you to modularly construct the text content of dialogue events and reduce error rates.

```cs
using RhythmBase.Global.Components.RichText;

RDDialogueExchange exchange = 
[
	new RDDialogueBlock()
	{
		Character = "Paige",
		Expression = "neutral",
		Content = RDLine<RDDialoguePhraseStyle>.Deserialize("Hel<color=#00FF00>lo [2]<shake>Rhythm</color> Doctor</shake>!"),
	},
	new RDDialogueBlock()
	{
		Character = "Ian",
		Content = "Hello Paige!",
	},
	new RDDialogueBlock()
	{
		Character = "Paige",
		Expression = "happy",
		Content = new RDPhrase<RDDialoguePhraseStyle>("What a good day!")
		{
			Events =
			[
				new RDDialogueTone(RDDialogueToneType.VerySlow,6),
				new RDDialogueTone(RDDialogueToneType.Static,11),
			],
			Style = new RDDialoguePhraseStyle()
			{
				Volume = 0.5f,
				Bold = true,
			},
		}
	}
];

Console.WriteLine(exchange.Serialize());
// Paige_neutral:Hel<color=lime>lo [2]<shake>Rhythm</color> Doctor</shake>!
// Ian:Hello Paige!
// Paige_happy:<volume=0.5><bold>What a[vslow] good[static] day!</volume></bold>
```

### Easing

After importing `RhythmBase.Global.Components.Easing`, you can easily use the `EaseType` enum constants.  
You can also use the extension method `Calculate()` to quickly compute eased values.

```cs
using RhythmBase.Global.Components.Easing;

double var1 = EaseType.InSine.Calculate(0.25);
double var2 = EaseType.Linear.Calculate(0.5, 4, 9);

Console.WriteLine(var1); // 0.07612046748871326
Console.WriteLine(var2); // 6.5
```

`EaseValue` is a simple yet powerful struct. Thanks to mfgujhgh's algorithm, by using the static method `Fit()` to process arbitrary data, `EaseValue` can fit a curve usable by Rhythm Doctor events with a given selection of `EaseType`.

```cs
using RhythmBase.Global.Components.Easing;

// Fit using a set of points and a threshold
EaseValue data1 = EaseValue.Fit([
	(0, 0),
	(1, 1)
], 3f);
// Fit using an initial value, a set of points, an optional list of ease types, and a threshold
EaseValue data2 = EaseValue.Fit(0, [
	(0, 0),
	(1, 1)
], [EaseType.Linear, EaseType.InSine], 3f);
// Get the value at a specific time from the easing data
float value = data1.GetValue(2.5f);
```

`RhythmBase.Extensions.EasePropertyExtensions` adds a `GetEaseProperties` method to types that implement `IEaseEvent`, which is used to obtain the easing curves for each property in a series of events.

```cs
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
using RhythmBase.RhythmDoctor.Extensions;

var deco = level.Decorations[0];

Move[] moves =
[
	new(){ Beat = level.BeatOf(1), Position = new(0, (RDExpression?)null), Duration = 1, Ease = EaseType.Linear, Angle = "2" },
	new(){ Beat = level.BeatOf(2.1f), Position = new(10, 90), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(2.2f), Position = new(90, 10), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(3), Position = new(10, 70), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(3.5f), Position = new(10, 10), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(3.8f), Position = new(30, 50), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(3.9f), Position = new(20, (RDExpression?)null), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(4.1f), Position = new(70, 20), Duration = 1, Ease = EaseType.Linear },
	new(){ Beat = level.BeatOf(4.4f), Position = new((RDExpression?)null, 0), Duration = 1, Ease = EaseType.Linear },
];

deco.AddRange(moves);

var eases = EasePropertyExtensions.GetEaseProperties(moves);

foreach(var e in eases)
	Console.WriteLine(e);
// [Position, RhythmBase.Components.Easing.EasePropertyPoint]
// [Scale, RhythmBase.Components.Easing.EasePropertySize]
// [Angle, RhythmBase.Components.Easing.EasePropertyFloat]
// [Pivot, RhythmBase.Components.Easing.EasePropertyPoint]
```

Types that implement the `IEaseProperty<>` interface, such as `EasePropertyColor`, `EasePropertyFloat`, `EasePropertyPoint`, and `EasePropertySize`, are used to store the content from a list of easing events.  
You can also get the value at a specific time.

> Before calling, please check the value type of the property and apply the appropriate type conversion.

```cs
using RhythmBase.Global.Components.Easing;

var result = ((EasePropertyPoint)eases["Position"]).GetValue(rdlevel.BeatOf(3.2f));

Console.WriteLine(result); // [59.759995, 21.840006]
```

### RDCode (Under Revision)
 
`RhythmBase.Components.RDLang.RDLang` provides a `TryRun()` method for evaluating Rhythm Doctor expressions.

> Note: If the expression is incorrect, it will return `false` and the result will be `0`.

`RDLang` also has a static field `Variables` for storing all commonly used variables and methods. Modifying this field before executing `TryRun` will affect the values during execution.  
`RDLang` also supports three common methods: `Rand()`, `atLeastRank()`, and `atLeastNPerfects()`. These methods can also be accessed via `RDVariables`.

```cs
using RhythmBase.RhythmDoctor.Components.RDLang;

RDLang.Variables.i[1] = 9;

RDLang.TryRun("numMistakesP2 = 3", out float result); // 3
RDLang.TryRun("numMistakesP2+i1", out result); // 12
RDLang.TryRun("atLeastRank(A)", out result); // 1
```
Since this library does not support dynamic level playback, you can use the following fields to simulate the effects of the last two functions:
- `atLeastRank()`  
	Use the `RDVariables.SimulateCurrentRank` property to change the simulated level rank state.  
	When the expression accesses the `atLeastRank()` method, this value will be used for simulation.
- `atLeastNPerfects()`
	Use the `RDVariables.SimulateAtLeastNPerfectsSuccessRate` property to change the simulated percentage of perfect hits.  
	When the expression accesses the `atLeastNPerfects()` method, this value will be used for simulation.

### Macro Events

Declare and use it like a new event, and it will generate level events according to your specifications!

By inheriting from the `MacroEvent` class and implementing new logic as shown below, you can freely manipulate it just like other events.  
Unlike `ForwardEvent`, it writes the specified sequence of events into the level instead of itself.

```cs
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Events;

public class GroupData1
{
	public RDSize Size { get; set; }
	public int RowIndex { get; set; }
}

public class MoveCameraRectangle : MacroEvent<GroupData1>
{
	public RDSize Size
	{
		get => Data.Size;
		set => Data.Size = value;
	}
	public Row Row
	{
		get => Rows?[Data.RowIndex] ?? [];
		set => Data.RowIndex = value.Index;
	}
	public MoveCameraRectangle() { }
	public override IEnumerable<BaseEvent> GenerateEvents()
	{
		yield return new MoveCamera() { Beat = new(1), Rooms = new(0), CameraPosition = new(50 - Size.Width / 2, 50 - Size.Height / 2), Duration = 1 };
		yield return new MoveCamera() { Beat = new(2), Rooms = new(0), CameraPosition = new(50 + Size.Width / 2, 50 - Size.Height / 2), Duration = 1 };
		yield return new MoveCamera() { Beat = new(3), Rooms = new(0), CameraPosition = new(50 + Size.Width / 2, 50 + Size.Height / 2), Duration = 1 };
		yield return new MoveCamera() { Beat = new(4), Rooms = new(0), CameraPosition = new(50 - Size.Width / 2, 50 + Size.Height / 2), Duration = 1 };
		yield return SetParent(new MoveRow() { Beat = new(1), RowPosition = new(50, 50), CustomPosition = true, Duration = 0 }, Row);
		yield return SetParent(new MoveRow() { Beat = new(1.001f), RowPosition = new(50 - Size.Width / 2, 50 - Size.Height / 2), CustomPosition = true, Duration = 1 }, Row);
		yield return SetParent(new MoveRow() { Beat = new(2), RowPosition = new(50 + Size.Width / 2, 50 - Size.Height / 2), CustomPosition = true, Duration = 1 }, Row);
		yield return SetParent(new MoveRow() { Beat = new(3), RowPosition = new(50 + Size.Width / 2, 50 + Size.Height / 2), CustomPosition = true, Duration = 1 }, Row);
		yield return SetParent(new MoveRow() { Beat = new(4), RowPosition = new(50 - Size.Width / 2, 50 + Size.Height / 2), CustomPosition = true, Duration = 1 }, Row);
	}
}
```

Note that this operation can be resource-intensive, so `LevelReadOrWriteSettings` disables this option by default.  
The underlying logic is that it attaches a special tag to each event in the sequence to mark it as "generated", so that these generated events can be cleaned up the next time events are read. For events that already have tags, there is an extra layer of logic to encapsulate them.

```cs
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;

LevelReadOrWriteSettings settings = new()
{
	EnableMacroEvent = true,
	InactiveEventsHandling = InactiveEventsHandling.Retain,
	Indented = true
};

using RDLevel level = RDLevel.Default;
level.Decorations.Add(new Decoration() { Room = RDRoomIndex.Room1 });
var re1 = new MoveCameraRectangle() { Beat = new(4), Size = new RDSize(80, 80) };
var re2 = new MoveCameraRectangle() { Beat = new(9), Y = 2, Size = new RDSize(20, 20) };
level.Add(re1);
level.Add(re2);
string levelJson = level.ToJsonString(settings);
Console.WriteLine(levelJson);
using RDLevel level2 = RDLevel.FromJsonString(levelJson, settings);

/* Will generate these events:
 * The following events will be generated:
 * {"bar":1,"beat":1,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","rooms":[0],"cameraPosition":[10,10],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":1,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[50,50]},
 * {"bar":1,"beat":1.001,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[10,10]},
 * {"bar":1,"beat":2,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","rooms":[0],"cameraPosition":[90,10],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":2,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[90,10]},
 * {"bar":1,"beat":3,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","rooms":[0],"cameraPosition":[90,90],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":3,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[90,90]},
 * {"bar":1,"beat":4,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","rooms":[0],"cameraPosition":[10,90],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":4,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000000","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[10,90]},
 * {"bar":1,"beat":4,"type":"TagAction","y":0,"Action":"Run","Tag":"$RhythmBase_MacroEvent$0000000000000000"},
 * {"bar":1,"beat":1,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","rooms":[0],"cameraPosition":[40,40],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":1,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[50,50]},
 * {"bar":1,"beat":1.001,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[40,40]},
 * {"bar":1,"beat":2,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","rooms":[0],"cameraPosition":[60,40],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":2,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[60,40]},
 * {"bar":1,"beat":3,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","rooms":[0],"cameraPosition":[60,60],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":3,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[60,60]},
 * {"bar":1,"beat":4,"type":"MoveCamera","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","rooms":[0],"cameraPosition":[40,60],"duration":1,"ease":"Linear"},
 * {"bar":1,"beat":4,"type":"MoveRow","y":-1,"tag":"$RhythmBase_MacroEvent$0000000000000001","row":0,"customPosition":true,"target":"WholeRow","rowPosition":[40,60]},
 * {"bar":2,"beat":1,"type":"TagAction","y":2,"Action":"Run","Tag":"$RhythmBase_MacroEvent$0000000000000001"},
 * {"bar":0,"beat":0,"type":"Comment","y":-1,"tab":"Song","show":false,"text":"$RhythmBase_MacroData$\n# Generated by RhythmBase #\n@RhythmBase.Test.Tutorial\u002BMoveCameraRectangle\n{\u0022Size\u0022:[80,80],\u0022RowIndex\u0022:0}\n\n{\u0022Size\u0022:[20,20],\u0022RowIndex\u0022:0}\n","color":"F2E644"}
 */
```

## Examples
### Merging Audio and Visual Levels

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;

// Read the visual effects level file
using RDLevel vfxLevel = RDLevel.FromFile(@"vfx.rdlevel");
// Read the audio level file
using RDLevel audioLevel = RDLevel.FromFile(@"beat.rdlevel");

// Remove all rows from the visual effects level
RowEventCollection[] vfxrows = [.. vfxLevel.Rows];
foreach (var row in vfxrows)
	vfxLevel.Rows.Remove(row);

// Copy all rows from the audio level into the new level
foreach (var row in audioLevel.Rows)
{
	// Copy row information
	Row row2 = new()
	{
		Rooms = row.Rooms,
		Character = row.Character,
		Sound = row.Sound,
		RowType = row.RowType
	};
	vfxLevel.Rows.Add(row2);

	// Copy events within the row
	BaseBeat[] evts = [.. row.OfEvent<BaseBeat>()];
	foreach (var evt in evts)
		row2.Add(evt);
}

// Copy necessary sound events
foreach (var sound in audioLevel.Where(e =>
	e.Tab == Tabs.Sounds &&	   // Event is in the Sounds tab
	e is not BaseRowAction &&	 // Sound events contain row events; adding row events here would cause reference errors
	e is not PlaySong &&		  // No need to copy PlaySong if the music is the same
	e is not SetCrotchetsPerBar)) // The timing of these events is independent of the number of crotchets per bar, so they don't need to be added
{
	vfxLevel.Add(sound);
}

// Write to a new level file
vfxLevel.SaveToFile(@"result.rdlevel");
```