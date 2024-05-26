# [RhythmBase](../../RadiationTherapy.md).[Utils](../namespace/Utils.md).TypeConvert  


### [RhythmBase.dll](../assembly/RhythmBase.md)  
类型转换模块  
  
## 方法  
  


### [EventType](../enum/EventType.md) ConvertToEnum(Type type)  
将类型转换为枚举常量。  
[EventType](../enum/EventType.md) ConvertToEnum\<T\>() where T : [BaseEvent](../class/BaseEvent.md), new()  
将类型转换为枚举常量。  
此方法不适用于基类。  
[EventType](../enum/EventType.md)[] ConvertToEnums\<T\>() where T : [BaseEvent](../class/BaseEvent.md)  
将类型转换为枚举常量集合。  
此方法适用于基类。  




### Type ConvertToType(string type)  

将类型名转换为对应 [BaseEvent](../class/BaseEvent.md) 类型。  




### Type ConvertToType([EventType](../enum/EventType.md) type)  

返回事件类型枚举常量对应的类型。  
此方法为扩展方法。