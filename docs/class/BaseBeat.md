# [RhythmBase](../../RhythmToolkit.md).[Events](../namespace/Events.md).BaseBeat  
### [RhythmBase.dll](../assembly/RhythmBase.md)
节拍事件的基类，继承自[BaseRowAction](BaseEvent.md)。所有节拍事件都直接继承于此。  
此类必需被继承。  

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | bool | Pulsable | 指示此节拍事件是否含有按拍。  
readonly | [Audio](../class/Audio.md) | BeatSound | 返回此节拍事件的节拍音效。  
readonly | [Audio](../class/Audio.md) | HitSound | 返回此节拍事件的按拍音效。  
readonly | [PlayerType](../enum/PlayerType.md) | Player | 返回此节拍事件的操控玩家。  

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | IEnumerable\<[Hit](../class/Hit.md)\> | HitTimes() | 返回此节拍事件的所有按拍点。  
