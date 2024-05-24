# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).RDBeat  
### [RhythmBase.dll](../assembly/RhythmBase.md)
节拍。  

## 构造

名称 | 说明
-|-
new([BeatCalculator](../class/BeatCalculator.md) calculator, float beatOnly) | 以纯节拍构造节拍对象。
new([BeatCalculator](../class/BeatCalculator.md) calculator, uint bar, float beat) | 以**小节-节拍**构造节拍对象。
new([BeatCalculator](../class/BeatCalculator.md) calculator, TimeSpan TimeSpan) | 以纯节拍构造节拍对象。


## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | float | BeatOnly | 节拍的纯节拍。  
readonly | (uint, float) | Multipy | 节拍的**小节-节拍**。
readonly | TimeSpan | TimeSpan | 节拍的时刻。

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | bool | Contains([Rooms](../class/Rooms.md) rooms) | 返回给定房间是否包含在此房间之内。  
default | bool | Item(byte index) | 返回或设置指定房间的可见性。