# [RhythmBase](../../RadiationTherapy.md).[Components](../namespace/Components.md).RDPointN  
表示不可空的

### [RhythmBase.dll](../assembly/RhythmBase.md)  

## 构造

### new([RDSizeN](../class/RDSizeN.ms) sz)
以尺寸构造点。

### new(float x, float y)
以横坐标和纵坐标构造点。

## 属性

### float X
横坐标。

### float Y
纵坐标。

## 方法  

### RDPointN Add(RDPointN pt, [RDSizeN](../class/RDSizeN.md))
与尺寸相加。

### RDPointN Add(RDPointN pt, [RDSizeNI](../class/RDSizeNI.md))
与尺寸相加。

### RDPointN Subtract(RDPointN pt, [RDSizeN](../class/RDSizeN.md))
与尺寸相减。

### RDPointN Subtract(RDPointN pt, [RDSizeNI](../class/RDSizeNI.md))
与尺寸相减。

### Offset([RDSizeN](../class/RDSizeN.md) p)
偏移指定尺寸。

### Offset(float x, float y)
偏移指定尺寸。