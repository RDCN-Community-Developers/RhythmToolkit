# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).Image  


### [RhythmBase.Asset.dll](../assembly/RhythmAsset.md)  
图像素材占位符。    
实现 [ISprite][i] 接口。  
  
## 属性和字段  
  


### string FilePath  

**readonly**  
文件路径。  
实现 [ISprite][i].FilePath 接口。  


### [RDPoint](../class/RDPoint.md) Size   

**readonly**  
文件尺寸。  
实现 [ISprite][i].Size 接口。  


### string Name  

**readonly**  
文件名。  
实现 [ISprite][i].Name 接口。  


### IEnumerable\<string\> Expressions  

**readonly**  
表情列表。  
实现 [ISprite][i].Expressions 接口。  


### SKBitmap Preview  

**readonly**  
预览图像。  
实现 [ISprite][i].Preview 接口。  
  
## 方法  
  


### bool CanRead(string path)  

**static**  
返回此文件是否可以读作 [Image]() 。  


### [Image]() FromPath(string path)  

**static**  
返回从此文件构建的一个新 [Image]() 实例。  
  
[i]: ../interface/ISprite.md