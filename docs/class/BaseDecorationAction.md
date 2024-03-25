# [RhythmBase](../../RhythmToolkit.md).[Events](../namespace/Events.md).BaseDecorationAction  
### [RhythmBase.dll](../assembly/RhythmBase.md)
装饰事件的基类，继承自 [BaseEvent](#baseevent)。所有装饰事件都直接继承于此。  
此类必需被继承。  

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [Decoration](Decoration.md) | Parent | 返回或设置装饰事件的父对象。  
readonly | string | Target | 返回装饰事件指向父对象的Id。  
readonly | Rooms | Rooms | 返回装饰事件的房间。  

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | T : [BaseDecorationAction](../class/BaseDecorationAction.md), new() | Copy\<T\> | 返回装饰事件的拷贝。  