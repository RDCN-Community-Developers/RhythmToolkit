# [RhythmBase](../../RadiationTherapy.md).[Events](../namespace/Events.md).BaseEvent    


### [RhythmBase.dll](../assembly/RhythmBase.md)  
关卡事件的基类，所有关卡事件都直接或间接继承于此。    
此类必需被继承。    
  
## 属性和字段  
  


### JObject _Origin  
(已弃用)用于存储关卡事件的原始反序列化对象。    


### Dictionary\<String, Object\> PrivateData  
用于存储用户自定义数据。    


### [EventType](../enum/EventType.md) Type  

**readonly**  
事件类型。    


### [Tabs](../enum/Tabs.md) Tab  

**readonly**  
事件栏位。    


### [RDBeat](../class/RDBeat.md) Beat  
返回或获取事件的节拍。  




### uint Y  

事件在事件栏内的高度。    


### [Rooms](Rooms.md) Rooms  

**readonly**  
事件所在的房间。    


### string Tag  
事件的标签。    


### [Condition](Condition.md) if  
事件的条件。    


### bool Active  
事件是否被激活。    
  
## 方法  
  




### T Clone\<T\>() where T : [BaseEvent](../class/BaseEvent.md)  

创建一个带有相同基础属性的指定事件类型副本实例。  


### [BaseEvent](../class/BaseEvent.md) Clone()  拷贝事件为一个新实例。