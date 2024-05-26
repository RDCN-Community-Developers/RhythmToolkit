# [RhythmBase](../namespaces.md).[Assets](../namespace/Assets.md).Sprite  


### [RhythmBase.dll](../assembly/RhythmBase.md)  
图像素材占位符。    
  
## 类型  
  
- [Clip](../class/Sprite.Clip.md)  
- [Frame](../class/Sprite.Frame.md)  
  
## 属性和字段  
  


### string FilePath  

**readonly**  
文件路径。  


### SKSizeI Size  

**readonly**  
文件尺寸。  


### string Name  

**readonly**  
文件名。  


### IEnumerable\<string\> Expressions  

**readonly**  
表情列表。  


### SKBitmap Preview  

**readonly**  
预览图像。  


### bool IsSprite  

**readonly**  
指示此实例是否为精灵图。  
若为假，则  


### [Frame](class/Sprite.Frame.md) Frames  
精灵图像。   


### SKBitmap Freeze  
精灵冻结图像资源。   




### uint? RowPreviewFrame  

精灵预览帧。  


### SKSizeI RowPreviewOffset  
精灵预览偏移。  


### SKRectI? Preview  
精灵预览图像在原图像中的位置。  


### HashSet\<[Clip](../class/Sprite.Clip.md)\> Clips  
精灵表情。  
  
## 方法  
  


### [Clip](../class/Sprite.Clip.md) AddBlankClip(string name)  
命名的空表情并返回此表情。  


### IEnumerable\<[Clip](../class/Sprite.Clip.md)\> AddBlankClipsForCharacter()  
添加用途为角色的精灵的必需表情并返回这些表情。  


### IEnumerable\<[Clip](../class/Sprite.Clip.md)\> AddBlankClipsForDecoration()  
添加用途为装饰的精灵的必需表情并返回这些表情。  


### [Sprite]() LoadFile(string path)  

**static**  
返回从此文件构建的一个新 [Sprite]() 实例。  


### WriteJson(string path)  
写入文件。   


### WriteJson(string path, [SpriteOutputSettings](../class/SpriteOutputSettings.md) settings)  
写入文件。  