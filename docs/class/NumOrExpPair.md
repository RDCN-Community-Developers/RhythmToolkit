# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).NumOrExpPair
### [RhythmBase.dll](../assembly/RhythmBase.md)
表达式对。  

## 构造
名称 | 说明
-|-
new([INumOrExp](../interface/INumOrExp.md) x, [INumOrExp](../interface/INumOrExp.md) y) | 以给定表达式构造表达式对。
new(string x, string y) | 以给定字符串构造表达式对。<br>此方法不会校验表达式。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [INumOrExp](../interface/INumOrExp.md) | x | 表达式的第一个值。
| | [INumOrExp](../interface/INumOrExp.md) | y | 表达式的第二个值。

## 方法
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | (float, float) | GetValue([Variables](../class/Variables.md) variables) | 以给定变量组获取值。