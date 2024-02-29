# RhythmToolkit 参考文档

## 目录

- [正文](#正文)
    - [枚举](#枚举)
    - [对象](#对象)
        - 事件基础
            - [BaseEvent](#baseevent)
            - [BaseBeatsPerMinute](#basebeatsperminute)
            - [BaseDecorationAction](#basedecorationaction)
            - [BaseRowAction](#baserowaction)
            - [BaseBeat](#basebeat)
            - [BaseRowAnimation](#baserowanimation)
        - 事件属性
            - [LimitedList&lt;T&gt;](#limitedlistt)
            - [INumberOrExpression](#inumberorexpression)
            - [Number](#number)
            - [Expression](#expression)
            - [NumberOrExpressionPair](#numberorexpressionpair)
            - [Pulse](#pulse)
            - [PanelColor](#panelcolor)
            - [Rooms](#rooms)
            - [Condition](#condition)
        - 关卡元素
            - [Variables](#variables)
            - [Decoration](#decoration)
            - [Row](#row)
            - [RDLevel](#rdlevel)
        - 工具类
            - RhythmBase.Util
                - [时间转换](#beatcalculator)
                - [坐标转换](#坐标转换)
                - [近似分数](#近似分数)
                - [事件克隆](#事件克隆)
                - [动态脚本](#动态脚本)
            - RhythmAsset.ImageUtils
                - [图像处理](#图像处理)
        - 配置类
            - RhythmBase.Util
                - [LevelInputSettings](#levelinputsettings)
            - RhythmAsset.ImageUtil
                - [SpriteInputSettings](#spriteinputsettings)
                - [SpriteOutputSettings](#spriteoutputsettings)
        - 图像相关(需引入`RhythmAsset`库)
            - [ISprite](#isprite)
            - [Sprite](#sprite)
            - [Image](#image)
            - [PlaceHolder](#placeholder)
            - [NullAsset](#nullasset)
        - 音频相关(需引入`RhythmAsset`库)
            - 待完成。
        - 本地化相关(需引入`RhythmLocalization`库)
            - [TranaslationManager](#tranaslationmanager)
- [示例](#示例)
    - [导入与导出](#导入与导出)
    - [移除未激活事件](#移除所有未激活事件)
    - [在每一拍按键](#在七拍子的每一拍按键)
    - [批量添加精灵](#批量添加精灵)
    - [最短按拍间隔](#获取关卡最短按拍间隔)
## 正文

### 枚举

`EventType`  
事件类型。  

`Tabs`  
事件栏位。  

`RoomIndex`  
房间序号。  

`RowType`  
轨道类型。  

---

### 对象
一些被重构的对象。  

---

#### `BaseEvent`  
关卡事件的基类，所有关卡事件都直接或间接继承于此。  
此类必需被继承。  

##### 属性和字段

**`JObject`** `_Origin`  
(已弃用)用于存储关卡事件的原始反序列化对象。  

**`Dictionary<String, Object>`** `PrivateData`  
用于存储用户自定义数据。  

*`readOnly`* [**`EventType`**](#eventtype) `Type`  
事件类型。  

*`readOnly`* [**`Tabs`**](#eventtype) `Type`  
事件栏位。  

**`float`** `BeatOnly`  
事件的节拍。在本项目中，所有事件的执行时刻以绝对节拍的方式记录。若需转换，请查阅[工具类](#beatcalculator)。  

**`uint`** `Y`  
事件在事件栏内的高度。  

*`readOnly`* [**`Rooms`**](#rooms) `Rooms`  
事件所在的房间。  

**`string`** `Tag`  
事件的标签。  

[**`Condition`**](#condition) `if`  
事件的条件。  

**`bool`** `Active` = `true`  
事件是否被激活。  

##### 方法

**`T`** `Copy<T>() where T : BaseEvent, new()`  
返回一个新的事件的实例，此实例带有与原实例相同的基础属性和字段。  

其他类型与游戏内编辑器上的关卡事件区别不大，暂不赘述。  

---

#### `BaseBeatsPerMinute`  
BPM事件的基类，继承自[`BaseEvent`](#baseevent)。所有BPM事件都直接继承于此。  
此类必需被继承。  

##### 属性和字段

**`float`** `BeatsPerMinute`  
BPM值。  

---

#### `BaseDecorationAction`  
装饰事件的基类，继承自[`BaseEvent`](#baseevent)。所有装饰事件都直接继承于此。  
此类必需被继承。  

##### 属性和字段

[**`Decoration`**](#decoration) `Parent`  
返回或设置装饰事件的父对象。  

*`readOnly`* **`string`** `Target`  
返回装饰事件指向父对象的Id。  

*`readOnly`* **`Rooms`** `Rooms`  
返回装饰事件的房间。  

##### 方法

**`T`** `Copy<T> where T : BaseDecorationAction, new()`  
返回装饰事件指向父对象的Id。  

---

#### `BaseRowAction`  
轨道事件的基类，继承自[`BaseEvent`](#baseevent)。所有轨道事件都直接继承于此。  
此类必需被继承。  

##### 属性和字段

[**`Row`**](#decoration) `Parent`  
轨道事件的父对象。  

*`readOnly`* **`string`** `Target`  
返回轨道事件指向父对象的Id。  

*`readOnly`* **`Rooms`** `Rooms`  
返回轨道事件的房间。  

##### 方法

**`T`** `Copy<T> where T : BaseRowAction, new()`  
返回轨道事件指向父对象的Id。  

---

#### `BaseBeat`  
节拍事件的基类，继承自[`BaseRowAction`](#baserowaction)。所有节拍事件都直接继承于此。  
此类必需被继承。  

##### 属性和字段

*`readOnly`* **`bool`** `Pulsable`  
指示此节拍事件是否含有按拍。  

##### 方法

**`IEnumerable<Pulse>`** `PulseTime()`  
返回此节拍事件的所有按拍点。  

---

#### `BaseRowAnimation`  
轨道动画事件的基类，继承自[`BaseRowAction`](#baserowaction)。所有轨道动画事件都直接继承于此。  
此类必需被继承。  

---

#### `LimitedList<T>`  
限制数量以及含有默认值的 List&lt;T&gt;

##### 构造

`new(uint count, T defaultValue) where T`  
以指定数量和指定默认值构造指定类型的列表。  

##### 属性和字段

`T` `DefaultValue`  
列表中元素的默认值。  
如果指定索引处未赋值(`null`也是赋值)即返回此值。  

---

#### `Variables`  
包含关卡开放的所能够调用的所有变量与自定义方法。  
其中，
+ 整数(`i`) 存储在列表 `LimitedList<int>(10, 0) i` 中
+ 浮点数(`f`) 存储在列表 `LimitedList<int>(10, 0) f` 中
+ 布尔值(`b`) 存储在列表 `LimitedList<float>(false, 0) b` 中
+ 其他自定义方法皆以其名称为字段。  

##### 属性和字段

**`object`** `Value(string variableName)`  
返回或设置`variableName`对应变量或自定义方法的值。  

---

#### `INumberOrExpression`  
一个接口，用于填充事件的表达式内容。  

##### 方法

**`string`** `Serialize()`  
返回此数值或表达式序列化后的字符串。  

**`float`** `GetValue(Variables variables)`  
返回此数值经变量或自定义方法求值后的结果(**NotImplemented**)。  

---

#### `Number`  
实现了`INumberOrExpression`接口。  

##### 方法

**`bool`** `CanPalse(string value)`  
返回此已序列化的字符串是否能被转换成数值形式。  

---

#### `Expression`  
实现了`INumberOrExpression`接口。  

##### 方法

**`bool`** `CanPalse(string value)`  
返回此已序列化的字符串是否能被转换成表达式形式(**NotImplemented**)。  

---

#### `NumberOrExpressionPair`  
表达式对。  

##### 构造

`new(INumberOrExpression, INumberOrExpression)`  
构造一个表达式对的实例。  

##### 属性和字段

[**`INumberOrExpression`**](#inumberorexpression) `X`  
表达式对的第一个值。  

[**`INumberOrExpression`**](#inumberorexpression) `Y`  
表达式对的第二个值。  

##### 方法

**`（float X, float Y)`** `GetValue(Variables variables)`  
返回此表达式对经变量或自定义方法求值后的结果(**NotImplemented**)。  

---

#### `Pulse`  
按拍点。  

##### 属性和字段

**`float`** `beatOnly`  
按拍点的节拍。在本项目中，所有事件的执行时刻以绝对节拍的方式记录。若需转换，请查阅[工具类](#beatcalculator)。  

**`float`** `hold`  
按拍点的按住时长。  

[**`BaseBeats`**](#basebeats) `Parent`  
引起此按拍点的节拍事件。  

*`readOnly`* **`bool`** `Holdable`  
此按拍点是否需要按住。  

---

#### `PanelColor`  
颜色控制。  

##### 属性和字段

**`SKColor`** `Color`  
返回或设置颜色。  
设置颜色后，`Panel`将会初始化为`-1`。  
若此实例的`EnableAlpha`属性为`false`。则设置后的实例的`Alpha`值始终为`255`。  

**`int`** `Panel`  
返回或设置调色板索引。  

*`readOnly`* **`bool`** `EnableAlpha`  
指示此颜色是否带有`Alpha`值。  

*`readOnly`* **`bool`** `EnablePanel`  
指示此颜色是否指向了调色板。  

---

#### `Rooms`  
房间。  

##### 属性和字段

*`readOnly`* **`bool`** `EnableTop`  
指示是否能够启用顶部房间。  

*`readOnly`* **`bool`** `Multipy`  
指示是否能够启用多个房间。  

*`default`* **`bool`** `Room(byte)`  
返回或设置指定房间是否启用。  

*`readOnly`* **`bool`** `Avaliable`  
指示此房间是否不可用。  

*`readOnly`* **`List<byte>`** `Rooms`  
返回所有可用的房间的序号构成的列表。  

*`static`* *`readonly`* **`Rooms`** `Default`  
返回一个新的不可用房间实例。  

##### 方法

**`bool`** `Contains(Rooms rooms)`  
返回给定房间是否包含在此房间之内。  

---

#### `Condition`  
房间。  

##### 属性和字段

**`list<(bool Enabled, BaseConditional conditional)>`** `ConditionLists`  
启用或禁用的条件列表。  

**`float`** `Duration`  
条件的启用时间。  

##### 方法

**`Condition`** `Load(IO.FileInfo text)`  
通过读取序列化后的条件字符串创建条件实例。  

---

#### `Decoration`  
装饰。  

##### 属性和字段

[**`ISprite`**](#isprite) `Parent`  
返回或设置装饰所用的父素材。  

*`readonly`* **`List<BaseDecorationAction>`** `Children`  
返回此装饰下的子事件。  

**`string`** `Id`  
返回或设置装饰的 Id。  

*`readonly`* **`Numerics.Vector2`** `Size`  
返回此装饰的尺寸。  

*`readonly`* **`IEnumerable<string>`** `Expressions`  
返回此装饰的表情名集合。  

**`ulong`** `Row`  
返回或设置装饰的Id。  

*`readonly`* **`Rooms`** `Rooms`  
返回装饰的序号。  

*`readonly`* **`ISprite`** `File`  
返回装饰文件素材对象。  

**`int`** `Depth`  
返回或设置装饰的深度。  

**`bool`** `Visible`  
返回或设置装饰的初始可见性。  

##### 方法

**`T`** `CreateChildren<T>(float beatOnly) where T : BaseDecorationAction, new()`  
返回一个新的事件实例，此事件的节拍位于指定位置，且已设置其`Parent`属性。  

**`T`** `CreateChildren<T>(BaseEvent item) where T : BaseDecorationAction, new()`  
返回一个新的事件实例，此事件的基础属性拷贝自参数，且已设置其`Parent`属性。  

**`Decoration`** `Copy()`  
返回此实例的一份拷贝。  

---

#### `Row`  
轨道。  

##### 属性和字段

*`readonly`* **`List<BaseRowAction>`** `Children`  
返回此轨道下的子事件。  

**`string`** `Character`  
返回或设置轨道的角色。  

**`RowType`** `RowType`  
返回或设置轨道的类型。  
**注意:修改此值会使`Children`清空。**

*`readonly`* **`sbyte`** `Row`  
返回轨道的序号。  

*`readonly`* **`Rooms`** `Rooms`  
返回轨道的房间。  

**`bool`** `HideAtStart`  
返回或设置轨道的初始可见性。  

**`PlayerMode`** `Player`  
返回或设置装饰的可见性。  

[**`Audio`**](#audio) `Sound`  
返回或设置轨道音效。  

**`bool`** `MuteBeats`  
返回或设置轨道静音。  

##### 方法

**`IEnumerable<Pulse>`** `PulseBeats()`  
返回此轨道的按拍点的集合。  

**`IEnumerable<BaseBeat>`** `PulseEvents()`  
返回此轨道的节拍事件的集合。  

**`T`** `CreateChildren<T>(float beatOnly) where T : BaseRowAction, new()`  
返回一个新的事件实例，此事件的节拍位于指定位置，且已设置其`Parent`属性。  

**`T`** `CreateChildren<T>(BaseEvent item) where T : BaseRowAction, new()`  
返回一个新的事件实例，此事件的基础属性拷贝自参数，且已设置其`Parent`属性。  

---

#### `RDLevel`  
节奏医生关卡
实现了`ICollection<BaseEvent>`接口。  

##### 属性和字段

*`readOnly`* **`Settings`** `Settings`  
关卡设置。  

*`readOnly`* [**`HashSet<ISprite>`**](#isprite) `Assets`  
图像素材集合。  

*`readOnly`* **`List<Rows>`** `Rows`  
轨道集合。  

*`readOnly`* **`List<Decorations>`** `Decorations`  
装饰集合。  

*`readOnly`* **`List<Conditionals>`** `Conditionals`  
条件集合。  

*`readOnly`* **`List<Bookmarks>`** `Bookmarks`  
书签集合。  

*`readOnly`* **`List<ColorPalette>`** `ColorPalette`  
调色盘集合。  

*`readOnly`* **`FileInfo`** `Path`  
关卡的文件信息。  

*`readOnly`* **`int`** `Count`  
事件总数量。  
(实现`ICollection<BaseEvent>.Count`接口)。  

*`readOnly`* **`bool`** `IsReadOnly`  
指示是否只读。  
(实现`ICollection<BaseEvent>.IsReadOnly`接口)。  

**`Variables`** `Variables`  
变量和自定义方法。  

##### 方法

**`IEnumerable<IGrouping<String, BaseEvent>>`** `GetTaggedEvents(string name, bool direct)`  
以标签名获取标签事件。  
+ **`string`** `name`  
标签名。  
+ **`bool`** `direct`  
若为`true`，则匹配标签名是`name`的事件，
否则匹配标签名包含`name`的事件。  

**`RDLevel`** `ReadFromString(string json, IO.FileInfo fileLocation, InputSettings.LevelInputSettings settings)`  
导入关卡。  

**`RDLevel`** `LoadFile(string filepath)`  
读取关卡文件。  
支持`rdlevel`,`rdzip`格式。  

**`RDLevel`** `LoadFile(string filepath, InputSettings.LevelInputSettings settings)`  
读取关卡文件。  
支持`rdlevel`,`rdzip`格式。  

**`void`** `SaveFile(string filepath)`  
保存关卡文件。  

**`void`** `SaveFile(string filepath, InputSettings.LevelInputSettings settings)`  
保存关卡文件。  

**`IEnumerable<Pulse>`** `GetPulseBeat()`  
返回关卡的按拍点集合。  

**`IEnumerable<BaseBeat>`** `GetPulseEvents()`  
返回关卡的节拍事件集合。  

**`void`** `Add(BaseEvent item)`  
向事件集合添加事件。  
(实现`ICollection(Of BaseEvent).Add(BaseEvent item)`接口)

**`void`** `AddRange(IEnumerable<BaseEvent> item)`  
向事件集合添加一系列事件。  

**`void`** `Clear()`  
清空`Events`集合。  
(实现`ICollection(Of BaseEvent).Clear()`接口)

**`bool`** `Contains(BaseEvent item)`  
返回关卡是否包含此事件。  
(实现`ICollection(Of BaseEvent).Contains(BaseEvent item)`接口)

**`IEnumerable<BaseEvent>`** `Where(Func<BaseEvent, bool> predicate)`  
以谓词筛选指定事件。  

**`IEnumerable<T>`** `Where<BaseEvent T>()`  
以类型筛选指定事件。  

**`IEnumerable<T>`** `Where<BaseEvent T>(Func<T, bool> predicate)`  
以类型和谓词筛选指定事件。  

**`BaseEvent`** `First()`  
获取关卡内第一个事件。  

**`BaseEvent`** `First(Func<BaseEvent, bool> predicate)`  
获取关卡内第一个满足谓词的事件。  

**`T`** `First<BaseEvent T>() where T : BaseEvent`  
获取关卡内第一个满足类型的事件。  

**`T`** `First<BaseEvent T>(Func<T, bool> predicate) where T : BaseEvent`  
获取关卡内第一个满足谓词和类型的事件。  

**`BaseEvent`** `FirstOrDefault()`  
获取关卡内第一个事件。  
若未找到则返回`null`。  

**`BaseEvent`** `FirstOrDefault(BaseEvent defaultValue)`  
获取关卡内第一个事件。  
若未找到则返回`defaultValue`。  

**`BaseEvent`** `FirstOrDefault(Func<BaseEvent, bool> predicate)`  
获取关卡内第一个满足谓词的事件。  
若未找到则返回`null`。  

**`BaseEvent`** `FirstOrDefault(Func<BaseEvent, bool> predicate, BaseEvent defaultValue)`  
获取关卡内第一个满足谓词的事件。  
若未找到则返回`defaultValue`。  

**`T`** `FirstOrDefault<BaseEvent T>() where T : BaseEvent`  
获取关卡内第一个满足类型的事件。  
若未找到则返回`null`。  

**`T`** `FirstOrDefault<BaseEvent T>(BaseEvent defaultValue) where T : BaseEvent`  
获取关卡内第一个满足类型的事件。  
若未找到则返回`defaultValue`。  

**`T`** `FirstOrDefault<BaseEvent T>(Func<T, bool> predicate) where T : BaseEvent`  
获取关卡内第一个满足谓词和类型的事件。  
若未找到则返回`null`。  

**`T`** `FirstOrDefault<BaseEvent T>(Func<T, bool> predicate, BaseEvent defaultValue) where T : BaseEvent`  
获取关卡内第一个满足谓词和类型的事件。  
若未找到则返回`defaultValue`。  

**`BaseEvent`** `Last(Func<BaseEvent, bool> predicate)`  
获取关卡内最后一个满足谓词的事件。  

**`T`** `Last<BaseEvent T>() where T : BaseEvent`  
获取关卡内最后一个满足类型的事件。  

**`T`** `Last<BaseEvent T>(Func<T, bool> predicate) where T : BaseEvent`  
获取关卡内最后一个满足谓词和类型的事件。  

**`BaseEvent`** `LastOrDefault()`  
获取关卡内最后一个事件。  
若未找到则返回`null`。  

**`BaseEvent`** `LastOrDefault(BaseEvent defaultValue)`  
获取关卡内最后一个事件。  
若未找到则返回`defaultValue`。  

**`BaseEvent`** `LastOrDefault(Func<BaseEvent, bool> predicate)`  
获取关卡内最后一个满足谓词的事件。  
若未找到则返回`null`。  

**`BaseEvent`** `LastOrDefault(Func<BaseEvent, bool> predicate, BaseEvent defaultValue)`  
获取关卡内最后一个满足谓词的事件。  
若未找到则返回`defaultValue`。  

**`T`** `LastOrDefault<BaseEvent T>() where T : BaseEvent`  
获取关卡内最后一个满足类型的事件。  
若未找到则返回`null`。  

**`T`** `LastOrDefault<BaseEvent T>(BaseEvent defaultValue) where T : BaseEvent`  
获取关卡内最后一个满足类型的事件。  
若未找到则返回`defaultValue`。  

**`T`** `LastOrDefault<BaseEvent T>(Func<T, bool> predicate) where T : BaseEvent`  
获取关卡内最后一个满足谓词和类型的事件。  
若未找到则返回`null`。  

**`T`** `LastOrDefault<BaseEvent T>(Func<T, bool> predicate, BaseEvent defaultValue) where T : BaseEvent`  
获取关卡内最后一个满足谓词和类型的事件。  
若未找到则返回`defaultValue`。  

**`void`** `CopyTo(BaseEvent() array, int arrayIndex)`  
将事件拷贝到数组。  
(实现`ICollection(Of BaseEvent).CopyTo()`接口)

**`bool`** `Remove(BaseEvent item)`  
移除事件。  
(实现`ICollection(Of BaseEvent).Remove()`接口)

**`int`** `RemoveAll(Predicate<BaseEvent> predicate)`  
移除满足谓词的事件。  

**`void`** `RefreshCPBs`  
刷新CPB事件。  
在每次更改关卡内CPB事件时调用此方法。  

---

### 工具

---

#### `BeatCalculator`  
提供一些时间转换方法。  

##### 构造

`new(IEnumerable<SetCrotchetsPerBar> CPBCollection, IEnumerable<BaseBeatsPerMinute> BPMCollection)`  
由BPM事件集合和CPB事件集合构造。  

`new(RDLevel level)`  
从关卡构造。  

##### 方法

*`static`* **`void`** `Initialize(IEnumerable<SetCrotchetsPerBar> CPBCollection)`  
初始化CPB集合。  

**`float`** `BarBeat_BeatOnly(uint bar, float beat)`  
将**小节-节拍**转换为**节拍**。  

**`TimeSpan`** `BarBeat_Time(uint bar, float beat)`  
将**小节-节拍**转换为**时间**。  

**`(uint bar, float beat)`** `BeatOnly_BarBeat(float beat)`  
将**节拍**转换为**小节-节拍**。  

**`TimeSpan`** `BeatOnly_Time(float beat)`  
将**节拍**转换为**时间**。  

**`(uint bar, float beat)`** `Time_BarBeat(TimeSpan time)`  
将**时间**转换为**小节-节拍**。  

**`float`** `Time_BeatOnly(TimeSpan time)`  
将**时间**转换为**节拍**。  

*`static`* **`float`** `BarBeat_BeatOnly(uint bar, float beat, IEnumerable<SetCrotchetsPerBar> SplittedBeats)`  
将**小节-节拍**转换为**节拍**。  

*`static`* **`(uint bar, float beat)`** `BeatOnly_BarBeat(float beat, IEnumerable<SetCrotchetsPerBar> SplittedBeats)`  
将**节拍**转换为**小节-节拍**。  

---

#### 坐标转换
提供一些坐标与百分数转换的方法。  

##### 方法

*`static`* **`(float? X, float? Y)`** `PercentToPixel((float? X, float? Y) point)`  
将**百分比**转换为**像素点**。  

*`static`* **`(float? X, float? Y)`** `PercentToPixel((float? X, float? Y) point, (float X, float Y) size)`  
将**百分比**依据尺寸转换为**像素点**。  

*`static`* **`(float? X, float? Y)`** `PixelToPercent((float? X, float? Y) point)`  
将**像素点**转换为**百分比**。  

*`static`* **`(float? X, float? Y)`** `PixelToPercent((float? X, float? Y) point, (float X, float Y) size)`  
将**像素点**依据尺寸转换为**百分比**。  

---

#### 近似分数

##### 方法

**`float`** `FixFraction(float number, uint splitBase)`  
返回给定小数在给定分母下的最近分数。  
这在实现关卡编辑器内的"磁铁"效果时非常有用。

---

#### 事件克隆

##### 方法

**`BaseEvent`** `Clone(BaseEvent e)`  
克隆整个事件。  

**`T`**  `Clone(T e) where T : BaseEvent`  
克隆整个事件。  

---

#### 动态脚本

##### 方法

**`Func<BaseEvent, bool>`** `FilterCodeCSharp(string code)`  
依据代码文本构建一个委托。  

---

#### 图像处理

##### 方法

**`SKBitmap`** `LoadImage(IO.FileInfo path)`  
读取图片为`SKBitmap`。  

**`void`** `SaveImage(SKBitmap image, IO.FileInfo path)`  
保存图片。  

**`SKBitmap`** `Outline(SKBitmap image)`  
为图片应用描边(**NotImplemented**)。  

**`SKBitmap`** `OutGlow(SKBitmap image)`  
为图片应用外发光(**NotImplemented**)。  

---

### 配置

---

#### `LevelInputSettings`  

##### 属性和字段

**`SpriteInputSettings`** `SpriteSettings`  
精灵图导入配置。  

---

#### `SpriteInputSettings`  

##### 属性和字段

**`bool`** `PlaceHolder`  
指定是否启用占位符。  

---

#### `SpriteOutputSettings`  

##### 属性和字段

**`bool`** `Sort` = `false`  
指定是否对导出的表情排序(同时有json文件中表情排序和帧序列排序)。  

**`bool`** `OverWrite` = `false`  
指定是否覆盖同名文件。  

**`OutputModes`** `OutputMode` = `OutputModes.HORIZONTAL`  
指定导出的图片的帧排序方式。  

**`bool`** `ExtraFile` = `false`  
指定是否在帧超出单张文件尺寸限制时导出额外文件。  

**`Vector2`** `LimitedSize` = `new Vector2(16384,16384)`  
指定导出图像的最大尺寸。  
若尺寸大于`(16384,16384)`会造成编辑器无法读取。  
最终图像尺寸由`LimitedSize`和`LimitedCount`共同决定。  

**`Vector2?`** `LimitedCount`  
指定导出图像帧的最多水平和垂直个数。  
最终图像尺寸由`LimitedSize`和`LimitedCount`共同决定。  

**`boolean`** `WithImage`  
指定是否导出图像。  

---

### 精灵
此库引用了`SkiaSharp`。  

---

#### `ISprite`  
精灵接口。  

##### 属性和字段

*`readOnly`* **`IO.FileInfo`** `FileInfo`  
文件信息。  

**`Vector2`** `Size`  
图像的尺寸。此属性返回在游戏内实际使用的像素尺寸。  

*`readOnly`* **`string`** `Name`  
文件名。  

*`readOnly`* **`IEnumerable<string>`** `Expressions`  
表情列表。  

*`readOnly`* **`SKBitmap`** `Preview`  
预览图片。  

---

#### `Sprite`  
继承了`ISprite`接口的精灵类型，用于读写精灵图文件以及提供属性和方法。  

##### 属性和字段

**`HashSet<SKBitmap>`** `Images`  
所有帧。  

**`SKBitmap`** `Images_Freeze`  
冻结帧。  

**`HashSet<SKBitmap>`** `Images_Glow`  
所有发光边缘帧。  

**`HashSet<SKBitmap>`** `Images_Outline`  
所有描边帧。  

**`SKBitmap`** `RowPreviewFrame`  
预览帧对象。  

**`Vector2`** `RowPreviewOffset`  
预览帧偏移。  

**`HashSet<Clip>`** `Clips`  
表情集合。  
表情属性请参阅[表情格式](https://rd.rdlevel.cn/ch7/25.html)。  

##### 方法

*`static`* **`bool`** `CanRead(IO.FileInfo path)`  
指示能否读取此精灵图文件。  

*`static`* **`Sprite`** `FromPath(IO.FileInfo path)`  
读取精灵图文件。  

*`static`* **`Sprite`** `FromImage(SKBitmap img, Vector2 size, ImageInputOption inputMode = ImageInputOption.HORIZONTAL)`  
返回一个用指定尺寸裁切的精灵图实例。  

**`Clip`** `AddBlankClip(string name)`  
添加空白表情。  

**`void`** `WriteJson(IO.FileInfo path)
以默认配置写入精灵图文件。  

**`void`** `WriteJson(IO.FileInfo path, SpriteOutputSettings settings)
以给定配置写入精灵图文件。  

---

#### `Image`  
继承了`ISprite`接口的图片类型，用于读写用于精灵的图片文件以及提供属性和方法。  

##### 方法

*`static`* **`bool`** `CanRead(IO.FileInfo path)`  
指示能否读取此精灵图文件。  

*`static`* **`Image`** `FromPath(IO.FileInfo path)`  
读取精灵图文件。  

---

#### `PlaceHolder`  
继承了`ISprite`接口的占位符类型，用于占位，缓解文件读取压力。  

##### 构造

`new(IO.FileInfo path)`  
以文件路径构建图片。  

##### 方法

**`ISprite`** `Read()`  
尝试读取此占位符并返回一个`ISprite`对象。

---

#### `NullAsset`  
继承了`ISprite`接口的空图像类型。  

---

### 本地化

#### `TranaslationManager`  
提供一些本地化工具。  

##### 构造

`new(IO.FileInfo filepath)`  
以一个json文件为基础构造。  

##### 方法

**`string`** `GetValue(object value)`  
尝试获取对象在文件中记录的字段。  
如果获取失败，则会在文件中创建字段并返回对象名。  
如果需要修改字段，在生成的文本中修改对应字段即可。  

---

## 示例

#### 导入与导出

```CS
//定义配置: 精灵占位符
LevelInputSettings setting = new LevelInputSettings{
    .SpriteSettings = new SpriteInputSettings{
        .PlaceHolder = true;
        }
    }
//导入关卡文件
RDLevel level = RDLevel.LoadFile(new IO.FileInfo("C:\\Document\\main.rdlevel"), setting);
//导出关卡文件
level.SaveFile(new IO.FileInfo("levelEdited.rdlevel"));
```  
```VB
'定义配置: 精灵占位符
Dim setting as New LevelInputSettings With {
    .SpriteSettings = New SpriteInputSettings With {
        .PlaceHolder = True
        }
    }
'导入关卡文件
Dim level As RDLevel = RDLevel.LoadFile(new IO.FileInfo("C:\Document\main.rdlevel"), setting)
'导出关卡文件
level.SaveFile(new IO.FileInfo("levelEdited.rdlevel"))
```  

#### 移除所有未激活事件
```CS
public void RemoveUnactive(RDLevel level){
    level.RemoveAll(i => !i.Active);
}
```  
```VB
Public Sub RemoveUnactive(level as RDLevel){
    Level.RemoveAll(Function(i) Not i.Active)
}
```  

#### 在七拍子的每一拍按键
```CS
public void PressOnEveryBeat(RDLevel level){
    //存储拆分出来的自由拍
    List<BaseBeat> SplittedBeats = new List<BaseBeat>();
    //存储处理好的按拍
    List<AddFreeTimeBeat> PulsingBeats = new list<AddFreeTimeBeat>();
    //拆分每一个七拍事件并存入集合
    foreach(var item in Level.Where<AddClassicBeat>)(){
        SplittedBeats.AddRange(item.Split());
        //使被处理的七拍事件不被激活
        item.Active = false;
    }
    //将每个自由拍转换成按拍点
    foreach(var item in SplittedBeats){
        //调用了基类方法以复制基本属性
        var n = item.Copy<AddFreeTimeBeat>();
        //设置按拍点
        n.Pulse = 6;
        //转换完成的按拍点
        PulsingBeats.Add(n);
    }
    //将所有按拍事件加入关卡
    Level.AddRange(PulsingBeats);
}
```  
```VB
Public Sub PressOnEveryBeat(level as RDLevel)
    '存储拆分出来的自由拍
    Dim SplittedBeats As New List(Of BaseBeat)
    '存储处理好的按拍
    Dim PulsingBeats As New List(Of AddFreeTimeBeat)
    '拆分每一个七拍事件并存入集合
    For Each item In Level.Where(Of AddClassicBeat)()
        SplittedBeats.AddRange(item.Split())
        '使被处理的七拍事件不被激活
        item.Active = False
    Next
    '将每个自由拍转换成按拍点
    For Each item In SplittedBeats
        '调用了基类方法以复制基本属性
        Dim n = item.Copy(Of AddFreeTimeBeat)
        '设置按拍点
        n.Pulse = 6
        '转换完成的按拍点
        PulsingBeats.Add(n)
    Next
    '将所有按拍事件加入关卡
    Level.AddRange(PulsingBeats)
End Sub
```  
#### 批量添加精灵
```CS
public void AddLotsOfDecos(RDLevel level, Decoration template, uint count){
    for(int i = 0; i <= count; i++){
        //添加精灵的拷贝
        level.Decorations.Add(template.Copy());
    }
}
```  
```VB		
Public Sub AddLotsOfDecos(level as RDLevel, template As Decoration, count As UInteger)
    For i As UInteger = 0 To count
        '添加精灵的拷贝
        level.Decorations.Add(template.Copy())
    Next
End Sub
```  

#### 获取关卡最短按拍间隔
(此示例并未考虑按拍与长按重叠的情况)
```CS
public IEnumerable<(Pulse, Pulse, TimeSpan)> GetLevelMinIntervalTime(RDLevel, level){
    //定义一个节拍计算器
    BeatCalculator Calculator = new BeatCalculator(level);
    //定义一个容器，用于存储所有按拍点
    List<Pulse> Pulse = new List<Pulse>;
    //定义一个容器，用于存储按拍间隔
    List<(Pulse, Pulse, TimeSpan) PulsesInterval = new List<(Pulse, Pulse, TimeSpan);
    //遍历存储所有按拍点
    foreach(var row in Level.Rows){
        Pulses.AddRange(row.PulseBeats());        
    }
    //按照按拍点去重
    Pulses = Pulses.GroupBy(i => i.BeatOnly).Select(i => i.First).OrderBy(i => i.BeatOnly).ToList();
    //存储相邻按拍点及其间隔
    for(int i = 0; i < Pulse.Count - 1){
        PulseInterval.Add((Pulses[i], Pulse[i + 1], Calculator.BeatOnly_Time(Pulses[i + 1].BeatOnly + Pulses[i + 1].Hold) - Calculator.BeatOnly_Time(Pulses[i].BeatOnly)));
    }
    //获得最小值
    var min = PulsesInterval.Min(i => i.Item3);
    //返回所有间隔等于最小值的项组成的集合
    return PulsesInterval.Where(i => i.Item3 == min);
}
```  
```VB
Public Function GetLevelMinIntervalTime(level as RDLevel) As IEnumerable(Of (Pulse, Pulse, TimeSpan))
    '定义一个节拍计算器
    Dim Calculator as New BeatCalculator(level)
    '定义一个容器，用于存储所有按拍点
    Dim Pulses As New List(Of Pulse)
    '定义一个容器，用于存储按拍间隔
    Dim PulsesInterval As New List(Of (Pulse, Pulse, TimeSpan))
    '遍历存储所有按拍点
    For Each row In Level.Rows
        Pulses.AddRange(row.PulseBeats())
    Next
    '按照按拍点去重
    Pulses = Pulses.GroupBy(Function(i) i.BeatOnly).Select(Function(i) i.First).OrderBy(Function(i) i.BeatOnly).ToList()
    '存储相邻按拍点及其间隔
    For i = 0 To Pulses.Count - 2
        PulsesInterval.Add((Pulses(i), Pulses(i + 1), Calculator.BeatOnly_Time(Pulses(i + 1).BeatOnly + Pulses(i + 1).Hold) - Calculator.BeatOnly_Time(Pulses(i).BeatOnly)))
    Next
    '获得最小值
    Dim min = PulsesInterval.Min(Function(i) i.Item3)
    '返回所有间隔等于最小值的项组成的集合
    Return PulsesInterval.Where(Function(i) i.Item3 = min)
End Function
```
在`BeatsViewer`，`RhythmTools`，`RhythmToolsUI`，`WaveProducer`内查看更多示例。  