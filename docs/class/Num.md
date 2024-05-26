# [RhythmBase](../../RadiationTherapy.md).[Components](../namespace/Components.md).Num  


### [RhythmBase.dll](../assembly/RhythmBase.md)  
可被表达式替换的数。    
实现了 [INumOrExp][nre] 接口。  
  
## 构造  
  


### new(float value)  
以给定字符串构造表达式。  
此方法不会校验表达式。  
  
## 属性和字段  
  


### float value  

**readonly**  
表达式的值。  
  
## 方法  
  


### float  


### GetValue([Variables][var] variables) 通过变量获得值。  
实现了 [INumOrExp][nre].GetValue([Variables][var] variables) 方法。  


### float? TryGetValue  
直接获得值。  
实现了 [INumOrExp][nre].TryGetValue() 方法。  
  
[var]: ../class/Variables.md  
[nre]: ../interface/INumOrExp.md