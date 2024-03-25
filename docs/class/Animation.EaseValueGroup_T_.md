# [RhythmBase](../namespaces.md).[Animation](../namespace/Animation.md).EaseValueGroup\<T\> where T : [BaseEvent](../class/BaseEvent.md)
### [RhythmBase.Animation.dll](../assembly/RhythmAnimation.md)
动画属性记录。

## 构造

名称 | 说明
-|-
new(T parent, [EaseType](../enum/EaseType.md) e, float duration, IEnumerables\<string\> propertyNames) | 以给定事件对象及其需记录的属性名列表构造动画属性记录对象。
new(T parent, [EaseType](../enum/EaseType.md) e, float duration, params string propertyNames[]) | 以给定事件对象及其需记录的属性名列表构造动画属性记录对象。

## 属性和字段

修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | [BaseEvent](../class/BaseEvent.md) | Parent | 此动画对象的父事件对象。
readonly | float | Start | 此动画的执行开始时间。
readonly | float | Duration | 此动画的执行持续时间。
readonly | float | End | 此动画的执行结束时间。
readonly | [EaseType](../enum/EaseType.md) | Ease | 此动画的缓动类型。
readonly | Dictionary\<string, [INumOrExp](../interface/INumOrExp.md)\> | Values | 此动画记录的属性。  

## 方法

修饰 | 类型 | 名称 | 说明
-|-|-|-
| | void | RefreshValue(Func\<float, [Variables](../class/Variables.md)\> realtimeVariables) | 以变量组刷新属性的值。
| | void | RefreshValue([Variables](../class/Variables.md)) | 以变量组刷新属性的值。
| | IDictionary\<string, float\> | GetValue(IDictionary\<string, float\> StartValues, float beat, Variables variables) | 获取指定时刻的属性值。