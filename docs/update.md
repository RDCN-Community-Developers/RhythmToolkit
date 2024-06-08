## [返回](../RadiationTherapy.md)  
# 更新日志 

### 2024/06/08

- 类型 [LevelOutputSettings](class/LevelOutputSettings.md) 添加了 Indented 字段，用于控制缩进。
- 修复了 [PlayExpression](namespace/Events.md) 事件表情总是为空的问题。

### 2024/06/06

**Nuget 版本 `1.0.3`**
- 将表达式部分移动到了 RhythmBase (测试阶段).
- 将几乎所有点与表达式的内容都替换为了
    - [RDExpression](class/RDExpression.md)
    - [RDPointN](class/RDPointN.md)*
    - [RDPointNI]()
    - [RDPoint]()
    - [RDPointI]()
    - [RDPointE]()
    - [RDSizeN]()
    - [RDSizeNI]()
    - [RDSize]()
    - [RDSizeI]()
    - [RDSizeE]() 等。
- 适配了版本 60。
- [RDBeat]() 做了很多修改。
- 类型 [Audio](class/Audio.md) 现在有初始值了。
- 优化了类型 [LimitedList]()。
- 类型 [OrderedEventCollection]() 添加了 105 个扩展方法及其重载。
- 类型 [Decoration]() 和 [Row]() 的 [CreateChildren\<T\>(item)]() 方法已弃用。
- 类型 [RDLevel]() 添加了 [ToJObject()]() 方法。
- 类型 [BaseEvent]() 移除了属性 [ParentLevel]()。

### 2024/05/30

- 添加了 [OrderedEventCollection]() 类型，并使 [`OrderedEventCollection<T>`](class/OrderedEventCollection.md) 继承于此类型。
- [RDBeat] 添加了 `BPM` 和 `CPB` 属性。
- 将大部分方法迁移到了 [Extensions](module/RhythmBase.Extension.md)，并：
    - 添加了 `Where` 方法 12 个
    - 添加了 `RemoveAll` 方法 31 个
    - 添加了 `TakeWhile` 方法 14 个
    - 添加了 `SkipWhile` 方法 14 个
    - 添加了 `IsInFrontOf` 方法
    - 添加了 `IsBehind` 方法
    - 移除了 `IndexOf` 方法。

### 2024/05/26  
  
- 修复了 [RDLevel](class/RDLevel.md) 的 `Row` 属性没有数量限制的问题。  

### 2024/05/24  
  
- 添加了 [RDBeat](class/RDBeat.md) 类型和 [RDRange](class/RDRange.md) 类型，以代替现有节拍计算方案。  
- 替换了 [BaseEvent](class/BaseEvent.md) 类型的 `BeatOnly` 字段，`BarBeat` 字段和 `TimeSpan` 字段为 [RDBeat](class/RDBeat.md) 类型的 `Beat` 字段。  
- 替换了 [Bookmark](class/Bookmark.md) 类型的 `BeatOnly` 字段为 [RDBeat](class/RDBeat.md) 类型的 `Beat` 字段。   
- [BeatCalculator](class/BeatCalculator.md) 类型添加了 3 个 `BeatOf` 方法以创建 [RDBeat](class/RDBeat.md) 类型。  
- [OrderedEventCollection](class/OrderedEventCollection.md) 类型添加了 12 个 `Where` 方法使得能够以节拍范围更精细地筛选事件。  
- [OrderedEventCollection](class/OrderedEventCollection.md) 类型调整了 `Where` 方法以节拍范围筛选事件的范围定义。现在可以使用 Range 的语法快速编写针对小节的事件筛选了。  
- 更新了[示例](examples.md)。  
  

### 2024/05/23  
  
- 修复了 [TintRows]() 和 [Comments]() 事件的移除问题。  
- 修复了 [Bookmark](class/Bookmark.md) 的序列化异常问题。  
- 修复了 [Characters](enum/Characters.md) 对当前游戏版本的适配问题。  
- 修复了 [RDLevel](class/RDLevel.md) 的 `Add()`方法不能添加轨道和装饰事件的问题。  
- 修复了 [RDLevel](class/RDLevel.md) 的 `CreateRow()` 方法和 `CreateDecoration()` 方法导致 `Parent` 缺失的问题。  
- 修复了 [PanelColor](class/PanelColor.md) 不能被替换的问题。  
- 修复了 [PlayExpression]() 空表情名导致的游戏卡死问题。  
- 修复了 [PlayAnimtion]() 空表情名导致的游戏无法读取问题。  
- 修复了 [CustomEvent](class/UnknownEvent.md) 无法读取的问题。  
  

### 2024/05/22  
  
- 修复了 [Commnets]() 事件的读取问题。现在若此事件不位于装饰栏内则 `Parent` 属性为空。  
- 修复了 [TintRows]() 事件的读取问题。现在若此事件为所有轨道染色则 `Parent` 属性为空。  
- 修复了 [RDLevel](class/RDLevel.md).Contains() 方法不判断 [Comments]() 的问题。  
- 读取时会忽略 `target` 字段为空的继承 [BaseDecorationAction](class/BaseDecorationAction.md) 事件。  
  

### 2024/05/21  
  
- 添加了 [Character](class/Character.md) 类型。  
- [Row](class/Row.md) 类型的 `Character` 字段替换为 [Character](class/Character.md) 类型。  
- 修复了 [Sprite](class/Sprite.md) 不读取文件夹下精灵文件的问题。  
  

### 2024/05/18  
  
- [UnknownEvent](class/UnknownEvent.md) 更名为 `CustomEvent`。  
- 更新了[示例](examples.md)。  
  

### 2024/05/17  
  
- 修复了 [Settings](class/Settings.md) 序列化时的枚举常量序列化为整数的问题。  
- 修复了没有链接至关卡的事件可能导致的空引用异常。  
- 添加了 [OrderedEventCollection](class/OrderedEventCollection.md) 类型，用于统一管理事件集合。  
- [RDLevel](class/RDLevel.md)，[Row](class/Row.md) 和 [Decoration](class/Decoration.md) 现在继承自 [OrderedEventCollection](class/OrderedEventCollection.md) 了。  
- 移除了 [Row](class/Row.md) 和 [Decoration](class/Decoration.md) 的 `Children` 属性。  
- 更新了[示例](examples.md)。  
  

### 2024/05/04  
  
- [BaseEvent](class/BaseEvent.md) 的 `Copy()` 方法更名为 `Clone()`。  
- 修复了 [BaseEvent](class/BaseEvent.md).Clone() 在克隆属性时 `If` 属性丢失和 `ParentLevel` 属性不能为空的问题。  
- 更新了[示例](examples.md)。  
  

### 2024/05/03  
  
- 将 [Assets](namespace/Assets.md) 命名空间内的所有内容移入了 [RhythmBase](assembly/RhythmBase.md)。  
- 现在所有图像素材都是 [Sprite](class/Sprite.md) 了。  
  

### 2024/03/29  
  
- 新增 [UnknownEvent](class/UnknownEvent.md) 类型，用以读写可能不支持的事件。  
- [ISprite](interface/ISprite.md) 的 Size 属性权限更改为 。  

**readonly**  
- [INumOrExp](interface/INumOrExp.md) 添加了 `TryGetValue()` 的尝试不通过变量获取值的方法。  
- 新增 [RDPoint](../class/RDPoint.md) 结构体，所有不可空数对都可与此结构体进行显式或隐式转换。  
  

### 2024/03/28  
  
。  
- `ConvertToType(EventType type)` 扩展方法从 [RhythmBase](namespaces.md).[Extensions](namespace/Extensions.md).[Extension](module/RhythmBase.Extension.md) 迁移至 [RhythmBase](namespaces.md).[Utils](namespace/Utils.md).[TypeConvert](module/TypeConvert.md) 。  
  

### 2024/03/27  
  
- [RDLevel](class/RDLevel.md) 添加了三个 Where 方法，用于对节拍范围进行快速筛选。  
    添加了两个构造方法。  
    添加了 ExtractEventsAt 方法。  
- 移除了 `NullAsset` 类型。现在尝试获取无引用的素材会返回 `null` (待测试)。  
  

### 2024/03/25  
  
- 翻新了文档。  
- 添加了[变量与动画属性解析模块](namespace/Animation.md)。  
- 重新整理了项目结构。  
- 为 `Comment` 和 `CallCustomMethod` 事件添加了一系列扩展方法。  
    现在可以用这些扩展方法快速创建自定义方法了。  
- 大量代码更改，已经不知道自己改了些啥了……  
  

### 2024/03/08  
  
- 更改 [Pulse](class/Hit.md) 为 [Hit](class/Hit.md)。  
- [Hit](class/Hit.md) 添加了一系列基本属性。  
  

### 2024/03/07  
  
- [BaseEvent](class/BaseEvent.md) 添加了一系列基本属性。    
- [BaseBeat](class/BaseBeat.md) 添加了一系列基本属性。    
- 修正了 [示例](examples.md) 上的部分错误。  
- 移除了 [条件](class/BaseConditional.md) 的 Children 属性。  
  

### 2024/03/05  
  
- 添加了[动画属性访问模块](namespace/Animation.md)。    
- [部分事件](interface/IEaseEvent.md)对动画模块进行了适配。  
- 将 [INumOrExpression](interface/INumOrExp.md) 下的类型替换为结构体。  
- 添加了实现了 [INumOrExpression](interface/INumOrExp.md) 的 [Function]() 类型，用于动态求值。  
- [PanelColor](#panelcolor) 添加了Value属性。  
- 缩小了 [RDLevel](#rdlevel) 类型的 Decorations 和 Rows 属性的访问权限。    
同时也移除了 [Decoration](class/Row.md) 和 [Row](class/Row.md) 的构造函数。  