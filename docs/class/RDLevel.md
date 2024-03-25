# [RhythmBase](../../RhythmToolkit.md).[LevelElements](../namespace/LevelElements.md).RDLevel
### [RhythmBase.dll](../assembly/RhythmBase.md)
节奏医生关卡。  
实现了ICollection\<[BaseEvent][E]\>接口。

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
readonly | IO.FileInfo | Path | 关卡的文件信息。
readonly | int | Count | 事件总数量。<br>实现ICollection\<[BaseEvent][E]\>.Count接口。
readonly | bool | Isreadonly | 指示是否只读。<br>实现ICollection\<[BaseEvent][E]\>.Isreadonly接口。
| | [Variables](../class/Variables.md) | Variables | 变量和自定义方法。

## 方法
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | [Decoration](../class/Decoration.md) | CreateDecoration([Rooms](../class/Rooms.md) room, ISprite parent, int depth = 0, bool visible = true) | 创建装饰。
| | [Decoration](../class/Decoration.md) | CopyDecoration([Decoration](../class/Decoration.md) decoration) | 复制装饰。
| | bool | RemoveDecoration([Decoration](../class/Decoration.md) decoration) | 移除装饰。
| | [Row](../class/Row.md) | CreateRow([Rooms](../class/Rooms.md) room, string character, bool visible = true) | 创建轨道。
| | bool | RemoveRow([Row](../class/Row.md) row) | 移除轨道。
| | IEnumerable\<IGrouping\<String, [BaseEvent][E]\>\> | GetTaggedEvents(string name, bool direct) | 以标签名获取标签事件。
| | RDLevel | ReadFromString(string json, IO.FileInfo fileLocation, [LevelInputSettings](../class/LevelInputSettings.md) settings) | 导入关卡。
| | RDLevel | LoadFile(IO.FileInfo filepath) | 读取关卡文件。<br>支持rdlevel,rdzip格式。
| | RDLevel | LoadFile(IO.FileInfo filepath, [LevelInputSettings](../class/LevelInputSettings.md) settings) | 读取关卡文件。<br>支持rdlevel,rdzip格式。
| | void | SaveFile(IO.FileInfo filepath) | 保存关卡文件。
| | void | SaveFile(IO.FileInfo filepath, [LevelOutputSettings](../class/LevelOutputSettings.md) settings) | 保存关卡文件。
| | IEnumerable\<[Hit](../class/Hit.md)\> | GetHitBeat() | 返回关卡的按拍点集合。
| | IEnumerable\<[BaseBeat](../class/BaseBeat.md)\> | GetHitEvents() | 返回关卡的节拍事件集合。
| | void | Add([BaseEvent][E] item) | 向事件集合添加事件。<br>实现ICollection\<[BaseEvent][E]\>.Add(BaseEvent item)接口
| | void | AddRange(IEnumerable\<[BaseEvent][E]\> item) | 向事件集合添加一系列事件。
| | void | Clear() | 清空Events集合。<br>实现ICollection\<[BaseEvent][E]\>.Clear()接口
| | bool | Contains([BaseEvent][E] item) | 返回关卡是否包含此事件。<br>实现ICollection\<[BaseEvent][E]\>.Contains([BaseEvent][E] item)接口
| | IEnumerable\<[BaseEvent][E]\> | Where(Func\<[BaseEvent][E], bool\> predicate) | 以谓词筛选指定事件。
| | IEnumerable\<T\> | Where\<T\>() where T : [BaseEvent][E] | 以类型筛选指定事件。
| | IEnumerable\<T\>| Where\<T\>(Func\<T, bool\> predicate) T : [BaseEvent][E] | 以类型和谓词筛选指定事件。
| | [BaseEvent][E] | First() | 获取关卡内第一个事件。
| | [BaseEvent][E] | First(Func\<[BaseEvent][E], bool\> predicate) | 获取关卡内第一个满足谓词的事件。
| | T | First\<T\>() where T : [BaseEvent][E]  | 获取关卡内第一个满足类型的事件。
| | T | First\<T\>(Func\<T, bool\> predicate)  T : [BaseEvent][E] | 获取关卡内第一个满足谓词和类型的事件。
| | [BaseEvent][E] | FirstOrDefault() | 获取关卡内第一个事件。<br>若未找到则返回null。
| | [BaseEvent][E] | FirstOrDefault(BaseEvent defaultValue) | 获取关卡内第一个事件。<br>若未找到则返回defaultValue。
| | [BaseEvent][E] | FirstOrDefault(Func\<[BaseEvent][E], bool\> predicate) | 获取关卡内第一个满足谓词的事件。<br>若未找到则返回null。
| | [BaseEvent][E] | FirstOrDefault(Func\<[BaseEvent][E], bool\> predicate, BaseEvent defaultValue) | 获取关卡内第一个满足谓词的事件。<br>若未找到则返回defaultValue。
| | T | FirstOrDefault\<T\>() where T : [BaseEvent][E] | 获取关卡内第一个满足类型的事件。<br>若未找到则返回null。
| | T | FirstOrDefault\<T\>(BaseEvent defaultValue) where T : [BaseEvent][E] | 获取关卡内第一个满足类型的事件。<br>若未找到则返回defaultValue。
| | T | FirstOrDefault\<T\>(Func\<T, bool\> predicate) where T : [BaseEvent][E] | 获取关卡内第一个满足谓词和类型的事件。<br>若未找到则返回null。
| | T | FirstOrDefault\<T\>(Func\<T, bool\> predicate, [BaseEvent][E] defaultValue) where T : [BaseEvent][E] | 获取关卡内第一个满足谓词和类型的事件。<br>若未找到则返回defaultValue。
| | [BaseEvent][E] | Last(Func\<[BaseEvent][E], bool\> predicate) | 获取关卡内最后一个满足谓词的事件。
| | T | Last\<T\>() where T : [BaseEvent][E] | 获取关卡内最后一个满足类型的事件。
| | T | Last\<T\>(Func\<T, bool\> predicate) where T : [BaseEvent][E] | 获取关卡内最后一个满足谓词和类型的事件。
| | [BaseEvent][E] | LastOrDefault() | 获取关卡内最后一个事件。<br>若未找到则返回null。
| | [BaseEvent][E] | LastOrDefault([BaseEvent][E] defaultValue) | 获取关卡内最后一个事件。<br>若未找到则返回defaultValue。
| | [BaseEvent][E] | LastOrDefault(Func\<[BaseEvent][E], bool\> predicate) | 获取关卡内最后一个满足谓词的事件。<br>若未找到则返回null。
| | [BaseEvent][E] | LastOrDefault(Func\<[BaseEvent][E], bool\> predicate, [BaseEvent][E] defaultValue) | 获取关卡内最后一个满足谓词的事件。<br>若未找到则返回defaultValue。
| | T | LastOrDefault\<T\>() where T : [BaseEvent][E] | 获取关卡内最后一个满足类型的事件。<br>若未找到则返回null。
| | T | LastOrDefault\<T\>([BaseEvent][E] defaultValue) where T : [BaseEvent][E] | 获取关卡内最后一个满足类型的事件。<br>若未找到则返回defaultValue。
| | T | LastOrDefault\<T\>(Func\<T, bool\> predicate) where T : [BaseEvent][E] | 获取关卡内最后一个满足谓词和类型的事件。<br>若未找到则返回null。
| | T | LastOrDefault\<T\>(Func\<T, bool\> predicate, [BaseEvent][E] defaultValue) where T : [BaseEvent][E] | 获取关卡内最后一个满足谓词和类型的事件。<br>若未找到则返回defaultValue。
| | void | CopyTo([BaseEvent][E][] array, int arrayIndex) | 将事件拷贝到数组。<br>实现ICollection\<[BaseEvent][E]\>.CopyTo()接口
| | bool | Remove([BaseEvent][E] item) | 移除事件。<br>实现ICollection\<[BaseEvent][E]\>.Remove()接口
| | int | RemoveAll(Predicate\<[BaseEvent][E]\> predicate) | 移除满足谓词的事件。

[E]: ../class/BaseEvent.md