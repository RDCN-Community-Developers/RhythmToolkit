# [RhythmBase](../../RadiationTherapy.md).[Extensions](../namespace/Extensions.md).AssetExtension  


### [RhythmBase.Animation.dll](../assembly/RhythmAnimation.md)  
针对事件的扩展方法。  
  
## 方法  
  


### [RDPoint](../class/RDPoint.md) VisualPosition([Move](../class/Move.md) e)  
返回事件的实际位置。  
如果 **`Position`, `Pivot`, `Angle`** 属性不为空，则不会执行。  
此方法为扩展方法。  


### [RDPoint](../class/RDPoint.md) VisualPosition([MoveRoom](../class/MoveRoom.md) e)  
返回事件的实际位置。  
如果 **`Position`, `Pivot`, `Angle`** 属性不为空，则不会执行。  
此方法为扩展方法。  


### MovePositionMaintainVisual([Move](../class/Move.md) e, [RDPoint](../class/RDPoint.md) target)  
保持实际位置不变，移动轴点到指定位置。  
如果 **`Position`, `Pivot`, `Angle`** 属性不为空，则不会执行。  
此方法为扩展方法。  


### MovePositionMaintainVisual([MoveRoom](../class/MoveRoom.md) e, [RDPoint](../class/RDPoint.md) target)  
保持实际位置不变，移动轴点到指定位置。  
如果 **`RoomPosition`, `Pivot`, `Angle`** 属性不为空，则不会执行。  
此方法为扩展方法。