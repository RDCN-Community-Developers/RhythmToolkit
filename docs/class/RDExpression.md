# [RhythmBase](../../RadiationTherapy.md).[Components](../namespace/Components.md).RDExpression  

### [RhythmBase.dll](../assembly/RhythmBase.md)  

## 构造

### new(float value)
以数值构造表达式。

### new(string value)
以表达式字符串构造表达式。

## 属性和字段

### bool IsNumeric
此表达式是否为数值。

### float NumericValue
此表达式的数值。

### string ExpressionValue
此表达式的表达式形式。

### Func\<[Variables](../class/Variables.md), float\> Expression
（未完成）此表达式经编译后的委托。

## 方法  

### float GetValue([Variables](../class/Variables.md) variables)  
通过变量获得值。  

### float? TryGetValue()  
尝试不通过变量获得值。  