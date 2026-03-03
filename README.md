![logo](RhythmBase_banner.png)


<p align="center">
  <a href="/LICENSE"><img src="https://img.shields.io/github/license/RDCN-Community-Developers/RhythmToolkit" alt="License"></a>
  <a href="https://www.nuget.org/packages/RhythmBase/"><img src="https://img.shields.io/nuget/v/RhythmBase?logo=nuget" alt="Nuget Download"></a>
  <img src="https://img.shields.io/nuget/dt/RhythmBase" alt="Downloads"/>
</p>


> If you find this project helpful, consider sponsoring via [爱发电 (Chinese mainland)](https://afdian.com/a/obugs) or [Ko-fi (Global)](https://ko-fi.com/obugs)!  


# RhythmBase

#### \[ English | [中文](./README_cn.md) \]

This project primarily serves **Rhythm Doctor** and **A Dance of Fire and Ice** level developers, aiming to provide an engine-agnostic, high-performance, systematic, and intuitive level editing proxy development library for developers.  
Thanks to the Rhythm Doctor fan community for their support of this project.  
You can see examples [here](/RhythmBase.Test/Tutorial.cs).

## Special Thanks
- Project Maintenance
    - [0x4D2](https://github.com/0x4D25F2) for substantial testing and feedback.
    - [mfgujhgh](https://github.com/mfgujhgh) for algorithm guidance.
- Sponsors
    - [来因洛特 | layinloty](https://space.bilibili.com/406743035)
    - [狗小白 | Dogbai](https://space.bilibili.com/1129425006)
    - [只能用宽判的屑 | kuanpan](https://space.bilibili.com/1928620300)
    - [mfgujhgh](https://space.bilibili.com/1369651)

| Project             | Description                                         | Status           | Link                                                                       | 
|---------------------|-----------------------------------------------------|------------------|:---------------------------------------------------------------------------|
| RhythmBase          | Core library for level editing proxy.                     | Maintained       | **You are here**                                                           |
| RhythmBase.View     | Draw all Rhythm Doctor events with SkiaSharp and TypeScript DOM versions. | Under Development | [Go there](https://github.com/OLDRedstone/RhythmBase.View)                 |
| RhythmBase.Addition | Extensions for the core library.                              | *Not disclosed*  | -                                                                          |
| RhythmBase.Interact | Interact with game level editors.                         | *Not disclosed*  | -                                                                          |
| RhythmBase.Hospital | Level review, hints, and assistance features.                         | *Not disclosed*  | -                                                                          |
| RhythmBase.Lite     | Lightweight version of RhythmBase.                  | Under Development | [Go there](https://github.com/RDCN-Community-Developers/RhythmToolkitLite) |
| RhythmBase.Control  | UI control library for level editing proxy.                                    | *Not disclosed*  | -                                                                          |

```mermaid
flowchart RL
RBLite[RhythmBase.Lite]
subgraph RD[Rhythm Doctor]
  RDLE[Rhythm Doctor Level Editor]
end
subgraph AD[Adofai]
  ADLE[Adofai Level Editor]
end
RDL([Rhythm Doctor Level])
ADL([Adofai Level])
subgraph RBTitle[RhythmBase]
  RB[RhythmBase]
  RBAdd[RhythmBase.Addition]
  RBInt[RhythmBase.Interact]
  RBHos[RhythmBase.Hospital]
  RBV[RhythmBase.View]
  RBVts["RhythmBase.View(TypeScript)"]
  subgraph RBC[RhythmBase.Control]
    RBCCore[RhythmBase.Control.Core]
    RBCWPF[RhythmBase.Control.WPF]
    RBCWF[RhythmBase.Control.WinForm]
    RBCAva[RhythmBase.Control.Avalonia]
  end
end

RBLite ---> RDL
RBCWPF & RBCWF & RBCAva --> RBCCore --> RBV
RBV & RBHos & RBAdd & RBInt --> RB ---> RDL & ADL
RBInt ---> RDLE --> RDL
RBInt ---> ADLE --> ADL
RBV <-.-> RBVts
```

### Core Features

#### Comprehensive Event System Support

RhythmBase provides a strongly-typed event model for both Rhythm Doctor and A Dance of Fire and Ice, covering all official event types and Adofai's advanced filter system, also providing compatibility for potential new event models in the future. Through type checking and intelligent hints, it eliminates runtime errors at the source.

#### Intelligent Event Processing

- **Event Discovery and Querying** - Flexible LINQ query support makes it easy to filter events by type or condition
- **Automatic Relationship Management** - Event binding system automatically maintains parent-child relationships without manual tree structure handling
- **Timeline Generation** - Built-in timeline management tools support advanced temporal sequences and control logic

#### Rich Toolkit

- **RichText Support** - Complete rich text syntax parsing and code generation for dialogue and title events
- **Easing Function Library** - Contains all in-game easing curves with support for custom interpolation functions
- **RDCode Integration** - Native support for Rhythm Doctor's code system, including syntax analysis and expression evaluation
- **Macro Event System** - Generate complex event sequences through code while maintaining full compatibility with original levels

### Read/Write Performance Comparison

This section compares the read/write performance using the level *The Power of Terry* (`the-powe-S7V1kg9RWYK.rdzip`).  
Operating System: Windows 25H2 26200.7462  
CPU: 12th Gen Intel® Core™ i7-12650H  
Memory: 16GB  

**Rhythm Doctor Level Editor**
- 🟥v1.0.3/r42(r65)
- Windows da9f047
- Uses `scrEditor.Decode` to open the `main.rdlevel` file, and calls `scrEditor.Encode` by pressing `Ctrl + S` in the editor UI.  

**RhythmBase**  
- 🟩.NET Standard 2.0 / 🟦.NET 8.0
- ░v1.3.4 / ▒v1.3.9
- Uses `RDLeve.FromFile` to read the complete `rdzip` file, and uses `RDLevel.SaveToFile` to save it as an `rdlevel` file.  
- Tested on 🟩.NET Framework 4.8.9221.0 / 🟦.NET 8.0.11

```mermaid
---
config:
    xyChart:
        height: 300
    themeVariables:
        xyChart:
            plotColorPalette: '#F00, #0F88, #0F8, #08F8, #08F'
---
xychart
  title "Read Time"
    y-axis "Duration (ms)"
    %% RDLE
    line [14441.1006, 16324.7944, 16264.1535, 16592.9209, 16411.6373, 16414.5378, 16274.7375, 16567.1659, 16288.4762, 16382.9809]
    %% RDTK .NET Standard 2.0 v1.3.4
    line [3273.9261, 2793.1168, 2796.0169, 2750.7386, 2757.9804, 2734.5127, 2715.0270, 2765.0712, 2749.9744, 2764.9854]
    %% RDTK .NET Standard 2.0 v1.3.9
    line [3148.9088,1226.1298,1054.2334,1134.9715,1033.4164,1030.4823,1088.7369,1178.1487,1120.97,1055.3118]
    %% RDTK .NET 8.0 v1.3.4
    line [1887.2656, 1353.3896, 1076.3732, 1121.0746, 1080.6883, 1074.4476, 1122.7722, 1091.6990, 1103.6302, 1149.2975]
    %% RDTK .NET 8.0 v1.3.9
    line [2367.0337,1056.9628,966.5576,1035.8415,913.1249,946.951,908.2692,917.0895,1036.6879,1048.7651]
```

```mermaid
---
config:
    xyChart:
        height: 300
    themeVariables:
        xyChart:
            plotColorPalette: '#F00, #0F88, #0F8, #08F8, #08F'
---
xychart
  title "Write Time"
    y-axis "Duration (ms)"
    %% RDLE
    line [242.5163, 280.5837, 243.9327, 238.8927, 244.9178, 243.3303, 251.8815, 243.9217, 331.3641, 265.0274]
    %% RDTK .NET Standard 2.0 v1.3.4
    line [513.1026, 361.6694, 350.3818, 347.8336, 349.8539, 356.7689, 353.0014, 350.7653, 354.2552, 348.2180]
    %% RDTK .NET Standard 2.0 v1.3.9
    line [403.8953,215.1384,235.0083,239.653,231.9253,215.1407,205.9433,235.8352,237.2161,252.8412]
    %% RDTK .NET 8.0 v1.3.4
    line [460.3911, 224.0186, 165.0900, 175.7229, 150.6010, 144.8973, 155.9880, 150.8031, 155.9035, 153.7161]
    %% RDTK .NET 8.0 v1.3.9
    line [391.9549,256.5255,113.1473,74.3901,75.7604,126.1112,107.5059,99.014,108.391,90.731]
```

### Platform and Language Interoperability

#### Cross-Platform Support

Thanks to .NET Standard 2.0 compatibility, RhythmBase runs seamlessly on multiple platforms:

- **Windows** - Via .NET Framework 4.6.1+ or .NET Core/8.0+
- **Linux** - Via .NET Core/8.0+ (with Mono compatibility)
- **macOS** - Via .NET Core/8.0+

This means regardless of your operating system, you get a consistent development experience and API interface, avoiding common cross-platform incompatibility issues.

#### Multi-Language Support

Thanks to cross-language interoperability technologies in the .NET ecosystem, you can call RhythmBase from different programming languages:

**C#, F#, VB.NET, C++/CLI** - Native support

**Python** - Via the [pythonnet](https://github.com/pythonnet/pythonnet) library, you can use RhythmBase directly in Python, suitable for:
- Writing automation scripts
- Batch processing level files
- Integrating with Python data analysis tools

Here are concrete code examples:

**C# Example** - Standard .NET development approach:

```cs
using RhythmBase.Global.Components.Vector;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;

LevelReadOrWriteSettings settings = new()
{
	UnreadableEventsHandling = UnreadableEventHandling.Store,
};
RDLevel rdlevel = RDLevel.Default;

foreach (Row row in rdlevel.Rows)
{
	MoveRow moveRow = new MoveRow()
	{
		Beat = new RDBeat(3),
		Position = new RDPointE(10, 20),
	};
}

rdlevel.SaveToFile("111.rdlevel");
```

**Python Example** - Using pythonnet to bridge .NET and Python ecosystems:

```py
# Assembly loading operations
import pythonnet
pythonnet.load('coreclr')
import clr
clr.AddReference('RhythmBase')

from RhythmBase.Global.Components.Vector import *
from RhythmBase.Global.Settings import *
from RhythmBase.RhythmDoctor.Components import *
from RhythmBase.RhythmDoctor.Events import *

settings = LevelReadOrWriteSettings()
settings.UnreadableEventsHandling = UnreadableEventHandling.Store
rdlevel = RDLevel.Default

for row in rdlevel.Rows:
    move_row = MoveRow()
    move_row.Beat = RDBeat(3)
    move_row.Position = RDPointE(10,20)

rdlevel.SaveToFile('111.rdlevel')
```

This flexibility enables RhythmBase to integrate into various toolchains and workflows, regardless of your preferred programming language or development environment.

### Advanced Features

#### Error Handling and Resilience

RhythmBase provides flexible error handling mechanisms:
- **Unreadable Event Mode** - When encountering unrecognized events, choose to store raw data rather than throwing exceptions
- **Custom Error Handling** - Full control over how to handle various exceptional situations

#### Type Safety and Compile-Time Checking

Benefits of C#'s strong type system:
- **Intellisense Support** - IDE automatically suggests all available event properties and methods
- **Refactoring Support** - Automatically updates related code when modifying event structures
- **Compile-Time Verification** - Eliminates runtime type errors

#### Performance Optimization

- **Zero-Copy Serialization** - Efficient binary format handling
- **Incremental Processing** - Stream processing support for large level files

#### Extensible Design

- **Partial Classes and Extension Methods** - Easy to extend functionality without modifying core library
- **Interface-Driven Design** - Interface-based event classification for custom processing logic
- **Plugin Architecture Foundation** - Solid foundation for building toolsets and extensions

#### Rich Data Structures and Algorithms

- **Generic Vector System** - Support for multiple-precision point, size, and rectangle structures (floating-point, integer, expression, optional value)
- **Curve Interpolation Engine** - Support for easing functions and custom curves for animation and numerical computation
- **Timeline Management System** - Automatic generation and maintenance of level timelines supporting complex animation and event sequences
- **Collections and Iterators** - Efficient collection implementations with LINQ query and lazy loading support

#### Complete Text and Serialization Support

- **RichText Engine** - Strongly-typed rich text structures supporting styles, events, and range operations
- **JSON Serialization** - High-performance serialization based on System.Text.Json supporting complex nested structures


## Project Direction and Positioning

This project was originally named `RhythmToolkit`, with the goal of developing a few small tools for *Rhythm Doctor* to simplify level processing and related tasks.  
As the project gradually matured, I found that most of the work was focused on reimplementing and optimizing the *Rhythm Doctor* level model. Its direction increasingly shifted toward becoming a foundational framework for other tools, while those original small utilities became relatively minor in comparison. Later, I also tried supporting the level model of *A Dance of Fire and Ice* (Adofai), so it was no longer exclusively for Rhythm Doctor (though Rhythm Doctor is still the primary focus).  
In addition, due to certain compatibility issues with SkiaSharp, I plan to separate the image-processing part into an independent module. For these reasons, the project was renamed to `RhythmBase`, and the more tool-oriented content was migrated to other repositories.  
Of course, you can still call it `RDTK` for short!  
To reduce project dependencies and solve compatibility issues, I moved some feature wrappers that I consider highly practical into an unreleased project, `RhythmBase.Addition`. This part can also serve as reference examples for using `RhythmBase`.