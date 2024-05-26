# [RhythmBase](../namespaces.md).[Animation](../namespace/Animation.md).EaseValueGroup\<T\> where T : [BaseEvent](../class/BaseEvent.md)  




### [RhythmBase.Animation.dll](../assembly/RhythmAnimation.md)  
动画属性记录。  
  


## 构造  
  




### new(T parent, [EaseType](../enum/EaseType.md) e, float duration, IEnumerables\<string\> propertyNames)  
以给定事件对象及其需记录的属性名列表构造动画属性记录对象。  




### new(T parent, [EaseType](../enum/EaseType.md) e, float duration, params string propertyNames[])  
以给定事件对象及其需记录的属性名列表构造动画属性记录对象。  
  


## 属性和字段  
  




### [BaseEvent](../class/BaseEvent.md) Parent  

**readonly**  
此动画对象的父事件对象。  




### float Start  

**readonly**  
此动画的执行开始时间。  




### float Duration  

**readonly**  
此动画的执行持续时间。  




### float End  

**readonly**  
此动画的执行结束时间。  




### [EaseType](../enum/EaseType.md) Ease  

**readonly**  
此动画的缓动类型。  




### Dictionary\<string, [INumOrExp](../interface/INumOrExp.md)\> Values  

**readonly**  
此动画记录的属性。    
  


## 方法  
  




### RefreshValue(Func\<float, [Variables](../class/Variables.md)\> realtimeVariables)  
以变量组刷新属性的值。  




### RefreshValue([Variables](../class/Variables.md))  
以变量组刷新属性的值。  




### IDictionary\<string, float\> GetValue(IDictionary\<string, float\> StartValues, float beat, Variables variables)  
获取指定时刻的属性值。