# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).PanleColor
### [RhythmBase.dll](../assembly/RhythmBase.md)
一个调色板颜色。

## 构造
名称 | 说明
-|-
new(bool enableAlpha) | 构造时决定此调色板是否支持 Alpha 通道。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | SKColor? | Color | 此调色板的颜色。<br>如果设置了调色板索引，则此值为 `null` 。
| | int | Panel | 此调色板的索引。<br>如果设置了调色板颜色，则此值为 `-1` 。
| | SKColor | Value | 此调色板最终的颜色。<br>如果设置了调色板索引，则返回索引颜色。<br>如果设置了调色板颜色，则返回此颜色。
| readonly | bool | EnableAlpha | 返回此调色板是否支持 Alpha 通道。
| readonly | bool | EnablePanel | 返回此调色板是否设置了索引。