# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).Image
### [RhythmBase.Asset.dll](../assembly/RhythmAsset.md)
图像素材占位符。  
实现 [ISprite][i] 接口。

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | IO.FileInfo | FileInfo | 文件路径。<br>实现 [ISprite][i].FileInfo 接口。
| | Numeric.Vector2 | Size | 文件尺寸。<br>实现 [ISprite][i].Size 接口。
readonly | string | Name | 文件名。<br>实现 [ISprite][i].Name 接口。
readonly | IEnumerable\<string\> | Expressions | 表情列表。<br>实现 [ISprite][i].Expressions 接口。
readonly | SKBitmap | Preview | 预览图像。<br>实现 [ISprite][i].Preview 接口。

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| static | bool | CanRead(IO.FileInfo path) | 返回此文件是否可以读作 [Image]() 。
| static | [Image]() | FromPath(IO.FileInfo path) | 返回从此文件构建的一个新 [Image]() 实例。

[i]: ../interface/ISprite.md