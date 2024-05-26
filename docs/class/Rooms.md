# [RhythmBase](../../RadiationTherapy.md).[Components](../namespace/Components.md).Rooms    

### [RhythmBase.dll](../assembly/RhythmBase.md)  
房间。    
  
## 枚举  
  
- [RoomIndex](../enum/Rooms.RoomIndex.md)  

## 构造

### new(bool enableTop, bool multipy)

构造时指定是否启用顶部房间和是否可多选房间。

### new(param byte[] rooms)

以给定数组构造房间。
  
## 属性和字段  
  

### bool EnableTop  

**readonly**  
指示是否能够启用顶部房间。    

### bool Multipy  

**readonly**  
指示是否能够启用多个房间。    

### bool Room(byte)  

**defalut**
返回或设置指定房间是否启用。    

### bool Avaliable  

**readonly**  
指示此房间是否不可用。    

### List\<byte\> Rooms  

**readonly**  
返回所有可用的房间的序号构成的列表。    

### [Rooms](../class/Rooms.md) Default  

**static**  

**readonly**  
返回一个新的不可用房间实例。    
  
## 方法  
  

### bool Contains([Rooms](../class/Rooms.md) rooms)  
返回给定房间是否包含在此房间之内。    

### default bool Item(byte index)  
返回或设置指定房间的可见性。