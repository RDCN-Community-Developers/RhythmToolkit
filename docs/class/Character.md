# [RhythmBase](../../RhythmToolkit.md).[LevelElements](../namespace/LevelElements.md).Character
### [RhythmBase.dll](../assembly/RhythmBase.md)
角色。

## 构造
名称 | 说明
-|-
new([Sprite](/class/Sprite.md) character) | 以给定资源创建角色。
new([Characters](/enum/Characters.md) character) | 以给定游戏自带角色创建角色。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | bool | IsCustom | 此对象是否是自定义角色。
readonly | [Characters](/enum/Characters.md)? | IsCustom | 游戏自带角色。<br>若 `IsCustom` 为 `true` 则为空。
readonly | [Sprite](/class/Sprite.md) | IsCustom | 自定义角色。<br>若 `IsCustom` 为 `false` 则为空。

