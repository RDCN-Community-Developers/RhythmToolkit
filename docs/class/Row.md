# [RhythmBase](../../RadiationTherapy.md).[LevelElements](../namespace/LevelElements.md).Row  




### [RhythmBase.dll](../assembly/RhythmBase.md)  
轨道。  
继承自 [OrderedEventCollection](../class/OrderedEventCollection.md)。  
  


## 枚举  
  
- [PlayerMode](../enum/Row.PlayerMode.md)  
  


## 属性和字段  
  




### [Character](class/Character.md) Character  
返回或设置轨道的角色。  




### [RowType](../enum/RowType.md) RowType  
返回或设置轨道的类型。  
注意:修改此值会使Children清空。  




### sbyte Row  

**readonly**  
返回轨道的序号。  




### [Rooms](../class/Rooms.md) Rooms  

**readonly**  
返回轨道的房间。  




### bool HideAtStart  
返回或设置轨道的初始可见性。  




### [PlayerMode](../enum/PlayerMode.md) Player  
返回或设置轨道的初始玩家轨道分配。  




### [Audio](../class/Audio.md) Sound  
返回或设置轨道音效。  




### bool MuteBeats  
返回或设置轨道静音。  
  
方法  
  




### IEnumerable\<[Hit](../class/Hit.md)\> PulseBeats()  
返回此轨道的按拍点的集合。  
  




### IEnumerable\<BaseBeat\> PulseEvents()  
返回此轨道的节拍事件的集合。  
  




### T CreateChildren\<T\>(float beatOnly) where T : BaseRowAction, new()  

返回一个新的事件实例，此事件的节拍位于指定位置，且已设置其Parent属性。  
  




### T CreateChildren\<T\>([BaseEvent](../class/BaseEvent.md) item) where T : BaseRowAction, new()  

返回一个新的事件实例，此事件的基础属性拷贝自参数，且已设置其Parent属性。