# [RhythmBase](../../RhythmToolkit.md).[Components](../namespace/Components.md).Exp
### [RhythmBase.dll](../assembly/RhythmBase.md)
表达式。  
实现了 [INumOrExp](../interface/INumOrExp.md) 接口。

## 构造
名称 | 说明
-|-
new(string value) | 以给定字符串构造表达式。<br>此方法不会校验表达式。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | string | value | 表达式的值。

## 方法
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | float | GetValue([Variables][var] variables) 通过变量获得值。<br>实现了 [INumOrExp][nre].GetValue([Variables][var] variables) 方法。
| | float? | TryGetValue | 返回 `null`。<br>实现了 [INumOrExp][nre].TryGetValue() 方法。