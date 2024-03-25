# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).Hit
### [RhythmBase.dll](../assembly/RhythmBase.md)
按拍点。

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | float | BeatOnly | 按拍点的节拍。在本项目中，所有事件的执行时刻以绝对节拍的方式记录。
readonly | (uint Bar, float Beat) | BarBeat | 返回事件的节拍。不建议频繁使用此属性。
readonly | TimeSpan | Time | 返回事件在关卡中的时间。不建议频繁使用此属性。
| | float | hold | 按拍点的按住时长。
| | [BaseBeat](../class/BaseBeat.md) | Parent | 引起此按拍点的节拍事件。
readonly | bool | Holdable | 此按拍点是否需要按住。