![logo](RhythmBase_banner.png)

<p align="center">
  <a href="/LICENSE"><img src="https://img.shields.io/github/license/RDCN-Community-Developers/RhythmToolkit" alt="License"></a>
  <a href="https://www.nuget.org/packages/RhythmBase/"><img src="https://img.shields.io/nuget/v/RhythmBase?logo=nuget" alt="Nuget Download"></a>
  <img src="https://img.shields.io/nuget/dt/RhythmBase" alt="Downloads"/>
</p>

# RhythmBase

This project serves **Rhythm Doctor** and **A Dance of Fire and Ice** level developers, aiming to provide a more systematic and intuitive level editing medium for developers.  
Thanks to the Rhythm Doctor fan community for their support of this project.  
You can see examples [here](/RhythmBase.Test/Tutorial.cs).

## Special Thanks

- [0x4D2](https://github.com/0x4D25F2) for amounts of testing and feedback.
- [mfgujhgh](https://github.com/mfgujhgh) for algorithm guidance.

| Project             | Description                                         | Status           | Link                                                                       | 
|---------------------|-----------------------------------------------------|------------------|:---------------------------------------------------------------------------|
| RhythmBase          | Core library for level editing.                     | WIP              | **You are here**                                                           |
| RhythmBase.View     | Draw all Rhythm Doctor event elements in SkiaSharp. | WIP              | [Go there](https://github.com/OLDRedstone/RhythmBase.View)                 |
| RhythmBase.Addition | Extensions for levels.                              | *Not disclosed*  | -                                                                          |
| RhythmBase.Interact | Interact with Level editor.                         | *Not disclosed*  | -                                                                          |
| RhythmBase.Hospital | Judgement logic for levels.                         | *Not disclosed*  | -                                                                          |
| RhythmBase.Lite     | Lightweight version of RhythmBase.                  | WIP              | [Go there](https://github.com/RDCN-Community-Developers/RhythmToolkitLite) |
| RhythmBase.Control  | Custom controls.                                    | *Not disclosed*  | -                                                                          |

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
```

### Read/Write Speed Comparison

This section compares the read and write speeds for the level *The Power of Terry* (`the-powe-S7V1kg9RWYK.rdzip`).  
Operating System: Windows 25H2 26200.7462  
CPU: 12th Gen Intel® Core™ i7-12650H  
RAM: 16GB  

**🟥Rhythm Doctor Level Editor v1.0.3/r42(r65) Windows da9f047**
- Utilizes `scrEditor.Decode` to open the `main.rdlevel` file and `scrEditor.Encode` by pressing `Ctrl + S` in the Editor Interface.  

**🟩RhythmBase v1.3.4 .NET Standard 2.0**
- Uses `RDLeve.FromFile` to read the complete `rdzip` file and `RDLevel.SaveToFile` to save it as an `rdlevel` file.  
- Tested in .NET Framework 4.8.9221.0

**🟦RhythmBase v1.3.4 .NET 8.0**
- Uses `RDLeve.FromFile` to read the complete `rdzip` file and `RDLevel.SaveToFile` to save it as an `rdlevel` file.  
- Tested in .NET 8.0.11


```mermaid
---
config:
    xyChart:
        height: 300
    themeVariables:
        xyChart:
            plotColorPalette: '#FF0000, #00FF88, #0088FF'
---
xychart
  title "Read Time"
    y-axis "Eclapsed (ms)"
    %% RDLE
    line [14441.1006, 16324.7944, 16264.1535, 16592.9209, 16411.6373, 16414.5378, 16274.7375, 16567.1659, 16288.4762, 16382.9809]
    %% RDTK .NET Standard 2.0
    line [3273.9261, 2793.1168, 2796.0169, 2750.7386, 2757.9804, 2734.5127, 2715.0270, 2765.0712, 2749.9744, 2764.9854]
    %% RDTK .NET 8.0
    line [1887.2656, 1353.3896, 1076.3732, 1121.0746, 1080.6883, 1074.4476, 1122.7722, 1091.6990, 1103.6302, 1149.2975]
```
```mermaid
---
config:
    xyChart:
        height: 300
    themeVariables:
        xyChart:
            plotColorPalette: '#FF0000, #00FF88, #0088FF'
---
xychart
  title "Write Time"
    y-axis "Eclapsed (ms)"
    %% RDLE
    line [242.5163, 280.5837, 243.9327, 238.8927, 244.9178, 243.3303, 251.8815, 243.9217, 331.3641, 265.0274]
    %% RDTK .NET Standard 2.0
    line [513.1026, 361.6694, 350.3818, 347.8336, 349.8539, 356.7689, 353.0014, 350.7653, 354.2552, 348.2180]
    %% RDTK .NET 8.0
    line [460.3911, 224.0186, 165.0900, 175.7229, 150.6010, 144.8973, 155.9880, 150.8031, 155.9035, 153.7161]
```

### Summary of Statistics

The performance benchmark results demonstrate substantial efficiency improvements across different RhythmBase versions compared to the Rhythm Doctor Level Editor v1.0.3/r42:

**RhythmBase v1.3.4 on .NET 8.0 (Optimal Performance):**
- **Read Speed:** Approximately **14.2x faster**, with an average read time of 1,149.30 ms versus the Rhythm Doctor Level Editor's 16,324.79 ms.
- **Write Speed:** Approximately **1.9x faster**, achieving an average write time of 144.90 ms compared to the Rhythm Doctor Level Editor's 280.58 ms.

**RhythmBase v1.3.4 on .NET Standard 2.0 (Moderate Performance):**
- **Read Speed:** Approximately **5.9x faster** than the Rhythm Doctor Level Editor, with an average read time of 2,760.54 ms.
- **Write Speed:** Approximately **1.4x slower** than the Rhythm Doctor Level Editor, with an average write time of 368.58 ms (a slight performance trade-off).

These results highlight the significant performance advantages of leveraging modern .NET frameworks, particularly .NET 8.0, for level file operations, especially for read-intensive workflows.

