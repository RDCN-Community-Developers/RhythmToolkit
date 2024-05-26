# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).[Sprite](../class/Sprite.md).Frame  


### [RhythmBase.dll](../assembly/RhythmBase.md)  
用于定位帧图像位置的图像资源集合。  
  
## 构造  
  


### new(SKSizeI)  
以给定尺寸构造一个空图像资源集合。  


### new(string)  
以给定路径构造一个图像资源集合。  
  
## 属性和字段  
  


### SKSizeI PortraitSize  

**readonly**  
图像的总尺寸大小。  


### SKBitmap Base  
基础图像资源。  


### SKBitmap Glow  
发光图像资源。  


### SKBitmap Outline  
描边图像资源。  
  
## 方法  
  


### SKRectI[] GetFrameRect()  
返回此表情的所有帧在原图中的位置。