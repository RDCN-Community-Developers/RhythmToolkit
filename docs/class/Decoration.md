# [RhythmBase](../../RhythmToolkit.md).[LevelElements](../namespace/LevelElements.md).Decoration
### [RhythmBase.dll](../assembly/RhythmBase.md)
装饰。  

##### 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | List\<[BaseDecorationAction](../class/BaseDecorationAction.md)\> | Children | 返回此装饰下的子事件。  
| | string | Id | 返回或设置装饰的 Id。  
readonly | Numerics.Vector2 | Size | 返回此装饰的尺寸。  
readonly | IEnumerable\<string\> | Expressions | 返回此装饰的表情名集合。  
| | ulong | Row | 返回或设置装饰的Id。  
readonly | [Rooms](../class/Rooms.md) | Rooms | 返回装饰的序号。  
readonly | [ISprite](../interface/ISprite.md) | File | 返回装饰文件素材对象。  
| | int | Depth | 返回或设置装饰的深度。  
| | bool | Visible | 返回或设置装饰的初始可见性。  

##### 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | T : [BaseDecorationAction](../class/BaseDecorationAction.md), new() | CreateChildren\<T\>(float beatOnly) | 返回一个新的事件实例，此事件的节拍位于指定位置，且已设置其Parent属性。  
| | T : [BaseDecorationAction](../class/BaseDecorationAction.md), new() | CreateChildren\<T\>([BaseEvent](../class/BaseEvent.md) item) | 返回一个新的事件实例，此事件的基础属性拷贝自参数，且已设置其Parent属性。  
| | [Decoration](../class/Decoration.md) | Copy() | 返回此实例的一份拷贝。  