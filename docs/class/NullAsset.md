# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).NullAsset
### [RhythmBase.dll](../assembly/RhythmBase.md)
空图像素材。
实现 [ISprite][i] 接口。

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | IO.FileInfo | FileInfo | 文件路径，返回 `null` 。<br>实现 [ISprite][i].FileInfo 接口。
| | Numeric.Vector2 | Size | 文件尺寸，返回 `new Numeric.Vector2()` 。<br>实现 [ISprite][i].Size 接口。
readonly | string | Name | 文件名，返回 ` ` 。<br>实现 [ISprite][i].Name 接口。
readonly | IEnumerable\<string\> | Expressions | 表情列表，返回 `new HaseSet<string>` 。<br>实现 [ISprite][i].Expressions 接口。
readonly | SKBitmap | Preview | 预览图像，返回 `new SKBitmap` 。<br>实现 [ISprite][i].Preview 接口。

[i]: ../interface/ISprite.md