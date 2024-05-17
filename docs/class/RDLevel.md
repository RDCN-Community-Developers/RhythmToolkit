# [RhythmBase](../../RhythmToolkit.md).[LevelElements](../namespace/LevelElements.md).RDLevel
### [RhythmBase.dll](../assembly/RhythmBase.md)
节奏医生关卡。  
继承自 [OrderedEventCollection](/class/OrderedEventCollection.md)。

## 构造

名称 | 说明
-|-
new() | 构造一个空关卡。
new(IEnumerable\<[BaseEvent][E]\> items) | 以给定一系列事件构造一个关卡。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | [Settings](../class/Settings.md) | Settings | 关卡设置。
readonly | HashSet\<[ISprite](../interface/ISprite.md)\> | Assets | 图像素材集合。
readonly | IReadonlyCollection\<[Row](../class/Row.md)\> | Rows | 轨道集合。
readonly | IReadonlyCollection\<[Decoration](../class/Decoration.md)\> | Decorations | 装饰集合。
readonly | List\<[BaseConditional](../class/BaseConditional.md)\> | Conditionals | 条件集合。
readonly | List\<[Bookmark](../class/Bookmark.md)\> | Bookmarks | 书签集合。
readonly | [LimitedList](../class/LimitedList.md)\<SKColor\> | ColorPalette | 调色盘集合。
readonly | string | Path | 关卡的文件信息。
readonly | int | Count | 事件总数量。<br>实现ICollection\<[BaseEvent][E]\>.Count接口。
readonly | bool | Isreadonly | 指示是否只读。<br>实现ICollection\<[BaseEvent][E]\>.Isreadonly接口。
| | [Variables](../class/Variables.md) | Variables | 变量和自定义方法。

## 方法
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [Decoration](../class/Decoration.md) | CreateDecoration([Rooms](../class/Rooms.md) room, ISprite parent, int depth = 0, bool visible = true) | 创建装饰。
| | [Decoration](../class/Decoration.md) | CloneDecoration([Decoration](../class/Decoration.md) decoration) | 复制装饰。
| | bool | RemoveDecoration([Decoration](../class/Decoration.md) decoration) | 移除装饰。<br>此方法会同时移除关卡内隶属于此装饰的事件。
| | [Row](../class/Row.md) | CreateRow([Rooms](../class/Rooms.md) room, string character, bool visible = true) | 创建轨道。
| | bool | RemoveRow([Row](../class/Row.md) row) | 移除轨道。<br>此方法会同时移除关卡内隶属于此轨道的事件。
| | IEnumerable\<IGrouping\<String, [BaseEvent][E]\>\> | GetTaggedEvents(string name, bool direct) | 以标签名获取标签事件。
| | RDLevel | ReadFromString(string json, string fileLocation, [LevelInputSettings](../class/LevelInputSettings.md) settings) | 导入关卡。
| | RDLevel | LoadFile(string filepath) | 读取关卡文件。<br>支持rdlevel,rdzip格式。
| | RDLevel | LoadFile(string filepath, [LevelInputSettings](../class/LevelInputSettings.md) settings) | 读取关卡文件。<br>支持rdlevel,rdzip格式。
| | void | SaveFile(string filepath) | 保存关卡文件。
| | void | SaveFile(string filepath, [LevelOutputSettings](../class/LevelOutputSettings.md) settings) | 保存关卡文件。
| | IEnumerable\<[Hit](../class/Hit.md)\> | GetHitBeat() | 返回关卡的按拍点集合。
| | IEnumerable\<[BaseBeat](../class/BaseBeat.md)\> | GetHitEvents() | 返回关卡的节拍事件集合。

[E]: ../class/BaseEvent.md