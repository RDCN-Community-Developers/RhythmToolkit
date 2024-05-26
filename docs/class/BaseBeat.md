# [RhythmBase](../../RadiationTherapy.md).[Events](../namespace/Events.md).BaseBeat    


### [RhythmBase.dll](../assembly/RhythmBase.md)  
节拍事件的基类，继承自[BaseRowAction](BaseEvent.md)。所有节拍事件都直接继承于此。    
此类必需被继承。    
  
## 属性和字段  
  


### bool Pulsable  

**readonly**  
指示此节拍事件是否含有按拍。    


### [Audio](../class/Audio.md) BeatSound  

**readonly**  
返回此节拍事件的节拍音效。    


### [Audio](../class/Audio.md) HitSound  

**readonly**  
返回此节拍事件的按拍音效。    


### [PlayerType](../enum/PlayerType.md) Player  

**readonly**  
返回此节拍事件的操控玩家。    
  
## 方法  
  


### IEnumerable\<[Hit](../class/Hit.md)\> HitTimes()  
返回此节拍事件的所有按拍点。    
