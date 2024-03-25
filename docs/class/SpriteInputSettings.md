# [RhythmBase](../namespaces.md).[Settings](../namespace/Settings.md).SpriteInputSettings
### [RhythmBase.Asset.dll](../assembly/RhythmAsset.md)

## 枚举

- [OutputModes](../enum/SpriteInputSettings.OutputMode.md)

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | bool | Sort | 返回或设置是否对输出的表情进行排序。<br>默认表情置顶，余下的表情按照字母顺序排序。
| | bool | OverWrite | 返回或设置是否覆写原素材文件。
| | [OutputMode](../enum/SpriteInputSettings.OutputMode.md) | OutputMode | 返回或设置输出图像的排列方式。
| | bool | ExtraFile | 返回或设置是否在超出图像限制时输出额外的文件。
| | Numerics.Vector2 | LimitedSize | 返回或设置素材文件的最大像素尺寸。
| | Numerics.Vector2? | LimitedCount | 返回或设置素材文件的最大长宽子图像个数。
| | bool | WithImage | 返回或设置是否输出图片文件。