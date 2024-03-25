# [RhythmBase](../../RhythmToolkit.md).[Animation](../namespace/Animation.md).EaseValueCalculator\<T\>
### [RhythmBase.Animation.dll](../assembly/RhythmAnimation.md)
缓动计算器。  

## 方法

名称 | 说明
-|-
new(IEnumaerable\<[EaseValueGroup](../class/Animation.EaseValueGroup.md)\<T\>\> valueList) | 以给定属性列表构造缓动计算器。

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | IEnumerable\<[EaseValueGroup](../class/Animation.EaseValueGroup.md)\<T\>\> | Values | 此类型的所有动画属性。  

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | IDictionary\<string, float\> | GetValue(float beat, Func\<float, [Variables](../class/Variables.md)\>, [EaseValueGroup\<T\>](../class/Animation.EaseValueGroup.md) defaultValue) 从缓动属性中获取指定时刻的值。