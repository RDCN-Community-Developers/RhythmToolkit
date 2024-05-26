# [RhythmBase](../../RadiationTherapy.md).[LevelElements](../namespace/LevelElements.md).Decoration  


### [RhythmBase.dll](../assembly/RhythmBase.md)  
装饰。    
继承自 [OrderedEventCollection](../class/OrderedEventCollection.md)。  
  
##

### 属性和字段  
  


### string Id  
返回或设置装饰的 Id。    


### [RDPoint](../class/RDPoint.md) Size  

**readonly**  
返回此装饰的尺寸。    


### IEnumerable\<string\> Expressions  

**readonly**  
返回此装饰的表情名集合。    




### ulong Row  

返回或设置装饰。    


### [Rooms](../class/Rooms.md) Rooms  

**readonly**  
返回装饰的序号。    


### [ISprite](../interface/ISprite.md) File  

**readonly**  
返回装饰文件素材对象。    


### int Depth  
返回或设置装饰的深度。    


### bool Visible  
返回或设置装饰的初始可见性。    
  
##

### 方法  
  




### T : [BaseDecorationAction](../class/BaseDecorationAction.md), new() CreateChildren\<T\>(float beatOnly)  

返回一个新的事件实例，此事件的节拍位于指定位置，且已设置其Parent属性。    




### T : [BaseDecorationAction](../class/BaseDecorationAction.md), new() CreateChildren\<T\>([BaseEvent](../class/BaseEvent.md) item)  

返回一个新的事件实例，此事件的基础属性拷贝自参数，且已设置其Parent属性。    


### [Decoration](../class/Decoration.md) Clone()  
返回此实例的一份拷贝。  