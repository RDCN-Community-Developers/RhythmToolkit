[English](README.md) | 中文

# RhythmBase 使用教程

本项目为节奏医生关卡开发者服务，旨在为开发人员提供更加系统、直观的关卡编辑媒介。  
感谢节奏医生饭制部玩家对这个项目的支持。  
欢迎来到 RhythmBase 教程。本指南将帮助您开始在项目中设置和使用 RhythmBase。

## 安装 Nuget 包

要安装 RhythmBase NuGet 包，请按照以下步骤操作：

1. 打开 Visual Studio 或使用命令行工具。
2. 在 Visual Studio 中，依次点击 **工具** > **NuGet 包管理器** > **程序包管理器控制台**。
3. 在控制台中输入以下命令：

    ```
    Install-Package RhythmBase -Version 1.2.0-rc2
    ```

4. 等待安装完成，并确保项目已引用所需的 NuGet 包。
5. 如果使用 .NET CLI，请输入：

    ```
    dotnet add package RhythmBase -version 1.2.0-rc2
    ```

## 编写
### 创建关卡

关卡是一个事件集合。

```cs
using RhythmBase.RhythmDoctor.Components;

using RDLevel emptyLevel = [];
Console.WriteLine(emptyLevel); // "" Count = 0
```

也可以使用自带的模板以创建具有基础事件的关卡。  
此模板即为节奏医生编辑器默认创建的关卡模板。

```cs
using RhythmBase.RhythmDoctor.Components;

using RDLevel defaultLevel = RDLevel.Default;
Console.WriteLine(defaultLevel); // "" Count = 3
```

### 读取和写入

可以直接以文件路径读取和导出文件。将以默认读写设置读写文件。
导出时不会打包为关卡包。

```cs
using RhythmBase.RhythmDoctor.Components;

// 直接读取关卡文件
using RDLevel rdlevel1 = RDLevel.FromFile(@"your\level.rdlevel");

// 读取关卡包文件
using RDLevel rdlevel2 = RDLevel.FromFile(@"your\level.rdzip");

// 读取关卡压缩包
using RDLevel rdlevel3 = RDLevel.FromFile(@"your\level.zip");

// 写入关卡文件
rdlevel1.SaveToFile(@"your\outLevel.rdlevel");
```

可以添加自定义读写设置 `LevelReadOrWriteSettings` 以读写关卡。

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Settings;

// 编写自定义读写设置
LevelReadOrWriteSettings settings = new()
{
    // 对未激活事件的处理方式
	InactiveEventsHandling = InactiveEventsHandling.Store,
    // 对读取出现异常的事件的处理方式
    // 常见于精灵事件未与精灵轨道绑定等
	UnreadableEventsHandling = UnreadableEventHandling.Store,
    // 是否启用缩进
	Indented = true,
};

using RDLevel rdlevel1 = RDLevel.FromFile(@"your\level.rdlevel", settings);
```

也可以生成 json 对象或 json 字符串以进行进一步操作。  

```cs
using RhythmBase.RhythmDoctor.Components;

LevelReadOrWriteSettings settings = new();

JsonDocument jobject = rdlevel.ToJsonDocument();
string json = rdlevel.ToJsonString(settings);
Console.WriteLine(jobject);
Console.WriteLine(json);
```

`LevelReadOrWriteSettings` 添加了 `BeforeReading`, `AfterReading`, `BeforeWriting`, `AfterWriting` 事件，分别会在关卡读取之前、之后，写入之前、之后触发。  
可在这些事件上添加监听以达到特定效果。

```cs
using RhythmBase.RhythmDoctor.Settings;

settings.AfterWriting += Settings_AfterReading;

// 将在写入结束时触发
void Settings_AfterReading(object? sender, EventArgs e)
{
	throw new NotImplementedException();
}

rdlevel.Write(@"your\outLevel.rdlevel", settings);
```

> 在读取关卡压缩包文件时请使用 `using` 语句或主动调用 `RDLevel.Dispose()` 方法以保证被解压的临时文件被及时销毁。

### Linq 查询

`OrderedEventCollection` 类型用于存储事件集合, `RDLevel` 继承此类型。  

可以使用针对节奏医生关卡事件的查询操作的扩展方法, 用以简化查询操作。  
例如通过事件类型及其父类型、事件实现的接口、节拍范围、自定义谓词等。  
提供 `AddRange()`, `RemoveRange()`, `OfEvent()`, `RemoveAll()`, `InRange` 等扩展方法。  

推荐使用这些经过效率优化的方法。

```cs
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;

// 查找在第 3 到第 5 小节、在事件栏的第 0 到第 2 行的移动轨道事件
var list = rdlevel
	.OfEvent<MoveRow>()
	.InRange(new(3, 1), new(5, 1)) // 第 3 到第 5 小节
	.Where(i => 0 <= i.Y && i.Y < 3);  // 在事件栏的第 0 到第 2 行
```

`Row` 和 `Decoration` 也继承 `OrderedEventCollection`, 所以轨道和精灵也支持这些扩展方法。

```cs
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;

// 查找在第 11 小节第 1 拍到第 13 小节第 1 拍的普通拍子事件
var list = rdlevel.Decorations[0]
	.OfEvent<AddClassicBeat>()
	.InRange(
		new Beat(11, 1), // 查找起点为第 11 小节第 1 拍
		new Beat(13, 1)  // 查找终点为第 13 小节第 1 拍
	);
```

### 创建节拍

`RDBeat` 是一个结构体, 它缓存三个信息 `BeatOnly`, `BarBeat`和`TimeSpan`。  

可以创建一个不与关卡相关的 `RDBeat` 实例, 但因为缺失和关卡的联系, 其功能可能不完善。  
可以查看它的 `IsEmpty` 属性以了解此实例是否可用。
在没有关卡联系的情况下, 调用它的 `ToString()` 方法会显示此实例拥有的信息和缺失的信息。  

```cs
using RhythmBase.RhythmDoctor.Components;

// 创建与关卡无关联的节拍
RDBeat beat1 = new(11);
RDBeat beat2 = new(2, 3);
RDBeat beat3 = new(TimeSpan.FromSeconds(11.45));

Console.WriteLine(beat1); // [10,?,?]
Console.WriteLine(beat2); // [?,(2, 3),?]
Console.WriteLine(beat3); // [?,?,00:00:11.4500000]
```

可以依靠 `BeatCalculator` 实例或 `RDLevel` 实例创建一个和关卡关联的 `RDBeat` 实例。  

其中 `BeatCalculator` 伴随 `RDLevel` 的创建而创建, 可通过 `RDLevel.Calculator` 访问。  
在有关卡联系的情况下, 调用它的 `ToString()` 方法会显示 `BarBeat` 属性。  
只有与关卡关联, 三个属性才能建立联系, 节拍才能参与所有运算。否则只有相应属性存在数据才能运算。  
关卡内事件和书签的节拍属性都是与关卡相关联的, 而移出的事件会断开与关卡的关联。  

```cs
using RhythmBase.RhythmDoctor.Components;

// 创建与关卡有关联的节拍
RDBeat beat1 = rdlevel.BeatOf(11);
RDBeat beat2 = rdlevel.Calculator.BeatOf(2, 3);
RDBeat beat3 = beat1 - 10 + TimeSpan.FromSeconds(11.45);

Console.WriteLine(beat1); // [2,3]
Console.WriteLine(beat2); // [2,3]
Console.WriteLine(beat3); // [3,4.083334]
```

在节拍之间参与运算时, 若都有所链接的关卡, 需确保其指向的关卡相同。  
可调用 `FromSameLevel()` 或 `FromSameLevelOrNull()` 方法检查其是否指向相同关卡。
可调用 `WithoutLink()` 返回一个脱离关卡的新节拍实例。

```cs
using RhythmBase.RhythmDoctor.Components;

RDBeat beat1 = rdlevel.BeatOf(1);
RDBeat beat2 = beat1.WithoutLink();

Console.WriteLine(beat1.FromSameLevel(beat2));       // False
Console.WriteLine(beat1.FromSameLevelOrNull(beat2)); // True
```

`BeatCalculator` 也带有时间转换的方法, 可以对不同的时间单位进行转换。

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;

(float, float) barbeat = rdlevel.Calculator.TimeSpanToBarBeat(TimeSpan.FromSeconds(19.19)); // (4, 8.983334)
```

`RDLevel` 带有一个和此实例关联的节拍数为 1 的默认节拍。

```cs
using RhythmBase.RhythmDoctor.Components;

RDBeat @default = RDLevel.DefaultBeat;
```

`RDRange` 是一个与 `Range` 相似的数据类型, 用以表示一个节拍范围。  
常用于查询事件。  

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extension;

var result = rdlevel.InRange(new RDRange(rdlevel.DefaultBeat + 10, null));
```

### 扩展数据类型

以 `RD` 开头, 带有 `Point`, `Size`, `Rect`, `RotatedRect` 的名称的类型都是 `RhythmBase` 里与平面几何相关的数据类型。  

后缀带有 `I` 的为整数类型, 其所有数据属性都为 `int`, 如 `RDPointI.X`。  
后缀带有 `N` 的为非空类型, 其所有数据属性都为不可空类型, 如 `RDSizeN.Height`。  
后缀带有 `E` 的为表达式类型, 其所有数据属性都为 `RDExpression`, 如 `RDRectE.Size`。  
`RotatedRect` 的 `Angle` 不受命名规则 `I` 约束, 其始终为浮点数类型。  

`RDExpression` 用以存储节奏医生表达式并尝试解析和求值（没做完）, 以字符串创建, 并支持简单的运算。  
底层为字符串拼接, 所以当然版本运算会导致表达式嵌套多层括号是正常现象。

```cs
using RhythmBase.RhythmDoctor.Components;

RDExpression exp1 = new("i2+1");
RDExpression exp2 = new(30);
RDExpression exp3 = new("25.5");

RDExpression result = exp1 - exp2 * exp3;

Console.WriteLine(result); // i2+1-765
```

### 创建事件和增删事件

所有事件都直接或间接实现 `IBaseEvent` 接口并继承 `BaseEvent` 抽象类型。  
可以将这些接口与抽象类型作为查询扩展方法的泛型参数以筛选事件。  
例如,   
`BaseRowAction`, `BaseDecorationAction` 分别是轨道事件和精灵事件,   
`IRoomEvent` 是拥有多房间属性的事件。  
创建事件所使用的节拍参数可以与关卡无关联, 当事件被添加进关卡时会创建与关卡的关联, 而移除关卡时也会切断关联。  
若不提供节拍参数则默认为关卡的第 1 拍。  
当调用事件的 `ToString()` 方法时, 将会以事件的节拍, 事件的类型, 事件的可显示数据的形式返回一个字符串。  

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

特别地，添加、更改、移除 `SetCrotchetsPerBar` 事件时会更新此事件之后的时间线，所以无需担心修改其数值时会影响事件的顺序、排布等；它们会按其自身的绝对节拍固定在相应的位置。关卡也会尝试增加新的 `SetCrotchetsPerBar` 事件或移除相同 `CrotchetsPerBar` 属性的相邻事件以维持其他片段的稳定。

轨道和精灵事件需要在相应的轨道或精灵上调用 `Add()` 进行添加, 而移除可以在轨道, 精灵或关卡的任意一处调用 `Remove()` 方法移除。  
重复添加不会有任何效果。  
事件类型 `Comment` 和 `TintRows` 不受此限制。  

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

### 向前兼容事件

如果此程序集没有需要的事件类型，可以继承 `ForwardEvent`, `ForwardRowEvent` 或 `ForwardDecorationEvent` 以实现类型。

```cs
using Newtonsoft.RhythmDoctor.Json.Linq;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Components;
  
// 创建 MyEvent 类型  
//   继承自 ForwardEvent 类型  
public class MyEvent : ForwardEvent
{
	// 重写属性  
	public override Tabs Tab => Tabs.Actions;

	// 实现的属性都需要和 CustomEvent.Data 字段内的数据绑定和判空。  

	// 实现一个 RDPointE 类型属性  
	public RDPointE? MyProperty
	{
		get
		{
			// 在 Data 字段内获取所需要的内容并判空  
            return ExtraData.TryGetValue("myProperty", out var jsonElement)
                ? jsonElement.Deserialize<RDPointE>()
                : null;
		}
		set
		{
			// 将内容保存在 Data 字段内  
            ExtraData["myProperty"] =
                value.HasValue ?
                JsonElement.Parse(
                    JsonSerializer.Serialize(value, Utils.GetJsonSerializerOptions())
                ) :
                default;
		}
	}

	// 在构造函数内初始化类型  
	public MyEvent()
	{
		// 初始化 RealType 属性。
		ActureType = nameof(MyEvent);
	}
}
```

编写好类型后可以像正常事件一样被读写。  
值得注意的是，`Type` 仍然是 `EventType.CustomEvent`, 而 `ActureType` 是自定义类型名。

```cs
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Components;

MyEvent myEvent = new();  
  
rdlevel.Add(myEvent);  
  
myEvent.Beat = new(8);  
  
Console.WriteLine(myEvent.Type);        // ForwardEvent  
Console.WriteLine(myEvent.ActureType);  // MyEvent  
```

如果读取关卡时意外出现未知事件类型，其也会被读取为相应的 `ForwardEvent`, `ForwardRowEvent` 或 `ForwardDecorationEvent` 类型事件。

### 事件类型与枚举

事件都拥有属性 `EaseType`, 可以通过 `EventTypeUtils` 内的方法转换得到对应的类型。

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;

Console.WriteLine(EventType.Tint.ToType());                                               // RhythmBase.Events.Tint
Console.WriteLine(EventTypeUtils.ToType("Tint"));                                         // RhythmBase.Events.Tint
Console.WriteLine(EventTypeUtils.ToEnum(typeof(Tint)));                                   // Tint
Console.WriteLine(EventTypeUtils.ToEnum<Tint>());                                         // Tint
Console.WriteLine(string.Join(", ", EventTypeUtils.ToEnums(typeof(IBarBeginningEvent))));  // PlaySong,SetCrotchetsPerBar, SetHeartExplodeVolume
Console.WriteLine(string.Join(", ", EventTypeUtils.ToEnums<IBarBeginningEvent>()));        // PlaySong,SetCrotchetsPerBar, SetHeartExplodeVolume
```

`EventTypeUtils` 也包含一些事件类型的归类，如:

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

### 富文本和对话组件

富文本组件位于 `RhythmBase.Components.RichText` 命名空间下，可以通过 `+` 运算组合自定义颜色的富文本。同时支持富文本的序列化和反序列化。  

`RDLine<>` 是一个完整的富文本。  
`RDPhrase<>` 是富文本的一个样式片段，其遵循单个样式。  
使用实现了 `IRDRichStringStyle<>` 的结构体以指明此富文本遵循的样式规则。下例的 `RDRichStringStyle` 即为仅带有颜色样式的富文本。  

都可以从 string 隐式转换。注意，转换的文字为不带样式的富文本。

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

`RDLine<>` 和 `RDPhrase<>` 都可以使用索引访问和修改其片段。

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

此包内也提供一整套适配节奏医生对话格式的对话组件，用以模块化构造对话事件的文本内容，减少错误率。

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

### 表达式（重写中）
 
`RhythmBase.Components.RDLang.RDLang` 提供一个 `TryRun()` 方法，用于运行节奏医生表达式。  

> 注意，若表达式不正确则会返回 `false`，同时结果为 `0`。  

`RDLang` 同时有一个静态字段 `Variables`，用于存储所有常用变量与方法。在执行 `TryRun` 之前修改字段会影响执行时的值。  
`RDLang` 也支持三个常用方法 `Rand()`，`atLeastRank()`，`atLeastNPerfects()`，这些方法都可以在 `RDVariables` 上正常访问。  

```cs
using RhythmBase.RhythmDoctor.Components.RDLang;

RDLang.Variables.i[1] = 9;

RDLang.TryRun("numMistakesP2 = 3", out float result); // 3
RDLang.TryRun("numMistakesP2+i1", out result); // 12
RDLang.TryRun("atLeastRank(A)", out result); // 1
```
由于此库不支持动态播放关卡，可使用以下字段对后两个函数的效果进行模拟：
- `atLeastRank()`  
	使用 `RDVariables.SimulateCurrentRank` 属性更改模拟关卡评级状态。  
	当表达式访问 `atLeastRank()` 方法时使用此值进行模拟。
- `atLeastNPerfects()`
	使用 `RDVariables.SimulateAtLeastNPerfectsSuccessRate` 属性更改模拟成功击拍百分比。  
	当表达式访问 `atLeastNPerfects()` 方法时使用此值进行模拟。

### 宏事件

像一个新事件一样声明并使用它，它会按照您指定的方式生成关卡事件！

像下面这样继承 `MacroEvent` 类，实现新的逻辑后，您可以像其他事件一样自由地操纵它。  
这与 `ForwardEvent` 不同，它会用指定的事件序列写入关卡，而不是它本身。

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

值得注意的是，这样的操作会比较消耗资源。所以 `LevelReadOrWriteSettings` 会默认禁用这个选项。  
其底层逻辑大致为，它会为事件序列中的每个事件附加一个特殊的标签以标记它是“被生成”的，以供下次读取事件时清理这些被生成的事件。对于其中已有标签的事件，它会有一层额外逻辑以封装这些事件。  

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

/* 会生成这些事件：
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


## 案例
### 合并采音关卡与视效关卡

```cs
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;

// 读取视效关卡文件
using RDLevel vfxLevel = RDLevel.FromFile(@"vfx.rdlevel");
// 读取采音关卡文件
using RDLevel audioLevel = RDLevel.FromFile(@"beat.rdlevel");

// 移除视效关卡内所有轨道
RowEventCollection[] vfxrows = [.. vfxLevel.Rows];
foreach (var row in vfxrows)
	vfxLevel.Rows.Remove(row);

// 复制采音关卡所有轨道的内容到新的关卡内
foreach (var row in audioLevel.Rows)
{
	// 复制轨道信息
	Row row2 = new()
	{
		Rooms = row.Rooms,
		Character = row.Character,
		Sound = row.Sound,
		RowType = row.RowType
	};
	vfxLevel.Rows.Add(row2);

	// 复制轨道内的事件
	BaseBeat[] evts = [.. row.OfEvent<BaseBeat>()];
	foreach (var evt in evts)
		row2.Add(evt);
}

// 复制需要的音效事件
foreach (var sound in audioLevel.Where(e =>
	e.Tab == Tabs.Sounds &&       // 事件位于音效栏中
	e is not BaseRowAction &&     // 音效事件内包含轨道事件，如果在这里添加轨道事件会导致引用出错
	e is not PlaySong &&          // 播放音乐事件相同的情况下不需要复制
	e is not SetCrotchetsPerBar)) // 复制事件的时间计算是无关四分音符数的，所以可以不用添加
{
	vfxLevel.Add(sound);
}

// 写入到新的关卡文件中
vfxLevel.SaveToFile(@"result.rdlevel");
```