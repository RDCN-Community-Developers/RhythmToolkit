# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).Sprite
### [RhythmBase.dll](../assembly/RhythmBase.md)
图像素材占位符。  

## 类型

- [Clip](../class/Sprite.Clip.md)
- [Frame](../class/Sprite.Frame.md)

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | string | FilePath | 文件路径。
readonly | SKSizeI | Size | 文件尺寸。
readonly | string | Name | 文件名。
readonly | IEnumerable\<string\> | Expressions | 表情列表。
readonly | SKBitmap | Preview | 预览图像。
readonly | bool | IsSprite | 指示此实例是否为精灵图。<br>若为假，则
| | [Frame](/class/Sprite.Frame.md) | Frames | 精灵图像。 
| | SKBitmap | Freeze | 精灵冻结图像资源。 
| | uint? | RowPreviewFrame | 精灵预览帧。
| | SKSizeI | RowPreviewOffset | 精灵预览偏移。
| | SKRectI? | Preview | 精灵预览图像在原图像中的位置。
| | HashSet\<[Clip](../class/Sprite.Clip.md)\> | Clips | 精灵表情。

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [Clip](../class/Sprite.Clip.md) | AddBlankClip(string name) | 添加一个以给定名称命名的空表情并返回此表情。
| | IEnumerable\<[Clip](../class/Sprite.Clip.md)\> | AddBlankClipsForCharacter() | 添加用途为角色的精灵的必需表情并返回这些表情。
| | IEnumerable\<[Clip](../class/Sprite.Clip.md)\> | AddBlankClipsForDecoration() | 添加用途为装饰的精灵的必需表情并返回这些表情。
static | [Sprite]() | LoadFile(string path) | 返回从此文件构建的一个新 [Sprite]() 实例。
| | void | WriteJson(string path) | 写入文件。 
| | void | WriteJson(string path, [SpriteOutputSettings](../class/SpriteOutputSettings.md) settings) | 写入文件。  