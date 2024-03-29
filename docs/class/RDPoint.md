# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).RDPoint
### [RhythmBase.dll](../assembly/RhythmBase.md)
二维数对。  
可与以下类型进行显式或隐式的相互转换。
- `Skiasharp` 下的 `SKPointI`, `SKPoint`, `SKSizeI`, `SKSize`,
- `System.Drawing` 下的 `Point`, `PointF`, `Size`, `SizeF`,
- `System.Numerics` 下的 `Vector2`
- [`RrhythmBase.Components`](../namespace/Components.md) 下的 [`NumOrExpPair`](../class/NumOrExpPair.md)
- 元组 `(int X, int Y)`, `(float X, float Y)`, `(double X, double Y)`

## 构造
名称 | 说明
-|-
new(string x, string y) | 以给定字符串构造表达式对。<br>此方法不会校验表达式。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | float | x | 表达式的第一个值。
| | float | y | 表达式的第二个值。