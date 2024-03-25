# [RhythmBase](../../RhythmToolkit.md).[Events](../namespace/Events.md).BaseRowAction  
### [RhythmBase.dll](../assembly/RhythmBase.md)
轨道事件的基类，继承自[BaseEvent](BaseEvent.md)。所有轨道事件都直接继承于此。  
此类必需被继承。  

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [Row](Row.md) | Parent | 轨道事件的父对象。  
readonly | string | Target | 返回轨道事件指向父对象的Id。  
readonly | [Rooms](Rooms.md) | Rooms | 返回轨道事件的房间。  