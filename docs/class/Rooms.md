# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).Rooms  
### [RhythmBase.dll](../assembly/RhythmBase.md)
房间。  

## 枚举

- [RoomIndex](../enum/Rooms.RoomIndex.md)

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | bool | EnableTop | 指示是否能够启用顶部房间。  
readonly | bool | Multipy | 指示是否能够启用多个房间。  
default | bool | Room(byte) | 返回或设置指定房间是否启用。  
readonly | bool | Avaliable | 指示此房间是否不可用。  
readonly | List\<byte\> | Rooms | 返回所有可用的房间的序号构成的列表。  
static readonly | [Rooms](../class/Rooms.md) | Default | 返回一个新的不可用房间实例。  

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | bool | Contains([Rooms](../class/Rooms.md) rooms) | 返回给定房间是否包含在此房间之内。  
default | bool | Item(byte index) | 返回或设置指定房间的可见性。