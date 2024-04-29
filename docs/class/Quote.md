# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).Quote
### [RhythmBase.dll](../assembly/RhythmBase.md)
对图像素材的引用。  
实现 [ISprite][i] 接口。

## 构造

名称 | 说明
-|-
new(string path) | 以给定路径创建一个对素材文件的引用。

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | string | FilePath | 文件路径。<br>实现 [ISprite][i].FileInfo 接口。
readonly | [RDPoint](../class/RDPoint.md) | Size  | 文件尺寸。<br>实现 [ISprite][i].Size 接口。
readonly | string | Name | 文件名。<br>实现 [ISprite][i].Name 接口。
readonly | IEnumerable\<string\> | Expressions | 表情列表。<br>实现 [ISprite][i].Expressions 接口。
readonly | SKBitmap | Preview | 预览图像。<br>实现 [ISprite][i].Preview 接口。

[i]: ../interface/ISprite.md