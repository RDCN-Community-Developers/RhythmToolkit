# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).Sprite
### [RhythmBase.Asset.dll](../assembly/RhythmAsset.md)
图像素材占位符。  
实现 [ISprite][i] 接口。

## 类型

- [Clip](../class/Sprite.Clip.md)

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | IO.FileInfo | FileInfo | 文件路径。<br>实现 [ISprite][i].FileInfo 接口。
readonly | [RDPoint](../class/RDPoint.md) | Size | 文件尺寸。<br>实现 [ISprite][i].Size 接口。
readonly | string | Name | 文件名。<br>实现 [ISprite][i].Name 接口。
readonly | IEnumerable\<string\> | Expressions | 表情列表。<br>实现 [ISprite][i].Expressions 接口。
readonly | SKBitmap | Preview | 预览图像。<br>实现 [ISprite][i].Preview 接口。
| | HashSet\<SKBitmap\> | Images | 精灵图像。 
| | SKBitmap | Images_Freeze | 精灵冻结图像。 
| | HashSet\<SKBitmap\> | Images_Glow | 精灵发光边缘图像。 
| | HashSet\<SKBitmap\> | Images_Outline | 精灵图像。 
| | SKBitmap | RowPreviewFrame | 精灵预览图像。
| | [RDPoint](../class/RDPoint.md) | RowPreviewOffset | 精灵预览偏移。
| | HashSet\<[Clip](../class/Sprite.Clip.md)\> | Clips | 精灵表情。

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
static | bool | CanRead(IO.FileInfo path) | 返回此文件是否可以读作 [Sprite]() 。
static | [Sprite]() | FromPath(IO.FileInfo path) | 返回从此文件构建的一个新 [Sprite]() 实例。
static | [Sprite]() | FromImage(SKBitmap img, [RDPoint](../class/RDPoint.md) size, [ImageInputOption](../enum/Assets.ImageInputOption.md) inputMode = [ImageInputOption](../enum/Assets.ImageInputOption.md).HORIZONTAL) | 返回从此图片文件构建的一个新 [Sprite]() 实例。
| | [Clip](../class/Sprite.Clip.md) | AddBlankClip(string name) | 添加一个以给定名称命名的空表情并返回此表情。
| | IEnumerable\<[Clip](../class/Sprite.Clip.md)\> | AddBlankClipsForCharacter() | 添加用途为角色的精灵的必需表情并返回这些表情。
| | IEnumerable\<[Clip](../class/Sprite.Clip.md)\> | AddBlankClipsForDecoration() | 添加用途为装饰的精灵的必需表情并返回这些表情。
| | void | WriteJson(IO.FileInfo path) | 写入文件。 
| | void | WriteJson(IO.FileInfo path, [SpriteOutputSettings](../class/SpriteOutputSettings.md) settings) | 写入文件。  

[i]: ../interface/ISprite.md