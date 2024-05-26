# [RhythmBase](../../RadiationTherapy.md).[LevelElements](../namespace/LevelElements.md).Character  




### [RhythmBase.dll](../assembly/RhythmBase.md)  
角色。  
  


## 构造  
  




### new([Sprite](class/Sprite.md) character)  
以给定资源创建角色。  




### new([Characters](enum/Characters.md) character)  
以给定游戏自带角色创建角色。  
  


## 属性和字段  
  




### bool IsCustom  

**readonly**  
此对象是否是自定义角色。  




### [Characters](enum/Characters.md)? IsCustom  

**readonly**  
游戏自带角色。  
若 `IsCustom` 为 `true` 则为空。  




### [Sprite](class/Sprite.md) IsCustom  

**readonly**  
自定义角色。  
若 `IsCustom` 为 `false` 则为空。  
  
