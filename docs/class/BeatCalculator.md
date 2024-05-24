# [RhythmBase](../../RhythmToolkit.md).[Utils](../namespace/Utils.md).BeatCalculator
### [RhythmBase.dll](../assembly/RhythmBase.md)
时间换算工具集。  
在本项目中，所有事件的时间以总节拍为单位。

## 方法

名称 | 说明
-|-
new(IEnumerable\<[SetCrotchetsPerBar][CPB]\> CPBCollection, IEnumerable\<[BaseBeatsPerMinute][BPM]\> BPMCollection) | 以给定 [SetCrotchetsPerBar][CPB] 事件集合和 [BaseBeatsPerMinute][BPM] 事件集合构造时间换算工具集实例。
new([RDLevel](../class/RDLevel.md) level) | 以给定关卡构造时间换算工具集实例。

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | float | BarBeat_BeatOnly(uint bar, float beat) | 将小节-节拍转换为总节拍数。
| | TimeSpan | BarBeat_Time(uint bar, float beat) | 将小节-节拍转换为时间。
| | (uint bar, float beat) | BeatOnly_BarBeat(float beat) | 将总节拍数转换为小节-节拍。
| | TimeSpan | BeatOnly_Time(float beat) | 将总节拍数转换为时间。
| | float | Time_BeatOnly(TimeSpan timeSpan) | 将时间转换为总节拍数。
| | (uint bar, float beat) | Time_BarBeat(TimeSpan timeSpan) | 将时间转换为小节-节拍。
| | TimeSpan | IntervalTime(float beat1, float beat2) | 获取两个节拍的时间差。
| | TimeSpan | IntervalTime([BaseEvent](../class/BaseEvent.md) event1, [BaseEvent](../class/BaseEvent.md) event2) | 获取两个事件的时间差。
static | float | Barbeat_BeatOnly(uint bar, float beat, IEnumerable\<[SetCrotchetsPerBar]()\> Collection) | 以给定 [SetCrotchetsPerBar][CPB] 事件集合将小节-节拍转换为总节拍数。
static | (uint bar, float beat) | BeatOnly_BarBeat(float beat, IEnumerable\<[SetCrotchetsPerBar]()\> Collection) | 以给定 [SetCrotchetsPerBar][CPB] 事件集合将总节拍数转换为小节-节拍。
| | [RDBeat](../class/RDBeat.md) | BeatOf(float beatOnly) | 从纯节拍创建节拍对象。
| | [RDBeat](../class/RDBeat.md) | BeatOf(uint bar, float beat) | 从纯节拍创建节拍对象。
| | [RDBeat](../class/RDBeat.md) | BeatOf(TimeSpan timeSpan) | 从纯节拍创建节拍对象。

[BPM]: ../class/BaseBeatsPerMinute.md
[CPB]: #