# [RhythmBase](../../RhythmToolkit.md).[Utils](../namespace/Utils.md).TypeConvert
### [RhythmBase.dll](../assembly/RhythmBase.md)
类型转换模块

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [EventType](../enum/EventType.md) | ConvertToEnum(Type type) | 将类型转换为枚举常量。
| | [EventType](../enum/EventType.md) | ConvertToEnum\<T\>() where T : [BaseEvent](../class/BaseEvent.md), new() | 将类型转换为枚举常量。<br>此方法不适用于基类。
| | [EventType](../enum/EventType.md)[] | ConvertToEnums\<T\>() where T : [BaseEvent](../class/BaseEvent.md) | 将类型转换为枚举常量集合。<br>此方法适用于基类。
| | Type | ConvertToType(string type) | 将类型名转换为对应 [BaseEvent](../class/BaseEvent.md) 类型。