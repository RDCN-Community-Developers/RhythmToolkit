![logo](RhythmBase_banner.png)

<p align="center">
  <a href="/LICENSE"><img src="https://img.shields.io/github/license/RDCN-Community-Developers/RhythmToolkit" alt="License"></a>
  <a href="https://www.nuget.org/packages/RhythmBase/"><img src="https://img.shields.io/nuget/v/RhythmBase?logo=nuget" alt="Nuget Download"></a>
  <img src="https://img.shields.io/nuget/dt/RhythmBase" alt="Downloads"/>
</p>

# RhythmBase

This project serves **Rhythm Doctor** and **A Dance of Fire and Ice** level developers, aiming to provide a more systematic and intuitive level editing medium for developers.  
Thanks to the Rhythm Doctor fan community for their support of this project.

## Special Thanks

- [0x4D2](https://github.com/0x4D25F2) for amounts of testing and feedback.
- [mfgujhgh](https://github.com/mfgujhgh) for alogrithm guidance.

| Project                     | Description                             | Status           | Link                                                                       | 
|-----------------------------|-----------------------------------------|------------------|----------------------------------------------------------------------------|
| RhythmBase                  | Core library for level editing.         |                  | **You are here**                                                           |
| RhythmBase.Addition         | Extensions for levels.                  | *Not disclosed*  | -                                                                          |
| RhythmBase.Interact         | Interact with Level editor.             | *Not disclosed*  | -                                                                          |
| RhythmBase.Hospital         | Judgement logic for levels.             | *Not disclosed*  | -                                                                          |
| RhythmBase.Lite             | Lightweight version of RhythmBase.      | Work in progress | [Go there](https://github.com/RDCN-Community-Developers/RhythmToolkitLite) |
| RhythmBase.Control.Core     | Core library for custom controls.       | *Not disclosed*  | -                                                                          |
| RhythmBase.Control.WPF      | WPF controls for level editing UI.      | *Not disclosed*  | -                                                                          |
| RhythmBase.Control.WinForms | WinForms controls for level editing UI. | *Not disclosed*  | -                                                                          |
| RhythmBase.Control.Avalonia | Avalonia controls for level editing UI. | *Not disclosed*  | -                                                                          |

```mermaid
flowchart RL
RB[RhythmBase]
RBAdd[RhythmBase.Addition]
RBInt[RhythmBase.Interact]
RBHos[RhythmBase.Hospital]
RBLite[RhythmBase.Lite]
RBCCore[RhythmBase.Control.Core]
RBCWPF[RhythmBase.Control.WPF]
RBCWF[RhythmBase.Control.WinForm]
RBCAva[RhythmBase.Control.Avalonia]

RBCWPF & RBCWF & RBCAva --> RBCCore
RBAdd & RBInt & RBCCore & RBHos --> RB
```