#### \[ [English](./VERSION.md) | 中文 \]

### 20260420 v1.3.11-alpha3
- 修复部分配置（如编码器、缩进等）不生效的异常
- 修复可空类型属性序列化时找不到对应的序列化器而默认写入空值的异常
- 添加统一的序列化器生成器配置接口

### 20260420 v1.3.11-alpla2
- 重构序列化器生成器
- 移除 MacroEvent 事件类型
- 补充 .NET Standard 2.0 兼容
- 修复特定条件下触发更改 BPM 导致的异常
- 添加 PlayerTypeGroup 构造方法
- 修复 PaletteColor 和 PaletteColorWithAlpha 索引色和自定义颜色反转的问题

### 20260405 v1.3.11-alpha1
- 同步 Rhythm Doctor 关卡版本
- 为 LevelReadSettings 和 LevelWriteSettings 添加自定义序列化接口

### 20260330 v1.3.10
- 修复以下事件模型的异常
    - CallCustomMethod 不再实现 IRoomEvent 接口
    - TagAction 适配新的枚举类型
    - FloatingText.Position 属性命名不统一
    - TextExplosion.Speed 属性默认值不正确
    - ReorderWindows.Order 属性默认值不正确
    - ReorderRooms.Order 属性默认值不正确
    - ReorderRow.Tab 属性值不正确
    - SetVFXPreset 属性可空
- 添加类型 RDExpressionBuilder，用于辅助表达式构造
- RDLevel 现已支持从 JsonDocument 加载
- RoomHeight 可以直接使用元组声明并隐式转换
- 缓存目录名添加合法性检查
- BaseDecorationAction 通过父 Decoration 获取其 Y 属性值
- 修复 EventEnumerator\<TEvent\> 边界问题
- 添加 ITintEvent 接口，用于统一 PaintHands/Tint/TintRows 事件属性
- 修复 AddClassicBeat 的 Splitted 扩展方法不按 Length 属性拆分的异常
- 修复调整拍号时 Bookmark 不会随之调整的问题
- 修复导出为压缩包时不会打包精灵素材的问题
- 为 .NET Standard 添加 RDLine<TStyle>.Deserialize 扩展方法适配
- 修复 BeatCalculator 计算异常
- RDBeat 类型可以用元组声明并隐式转换了
- 添加 Row 事件的 Y 属性指向 Row 在其所在房间的索引
- 修复 Order 类型无法表示窗口数量超过 4 的情况
- 添加 Corner 类型，用于适配 SetRoomPerspective

### 20260320 v1.3.9-patch2
- 修复 BeatCalculator 计算异常

### 20260319 v1.3.9-patch1
- 修复 .NET Standard 2.0 下 Nuget 错误打包异常

### 20260319 v1.3.9
- 将所有枚举类型移动到了父级命名空间下
- 修复在自动拍号修复时不会触发修改事件的异常
- 移除了弃用的方法

### 20260306 v1.3.9-alpha2
- 修复以下事件模型的异常
    - Move.Pivot 属性类型不正确
    - NarrateRowInfo.CustomRowLength 属性缺失
    - NewWindowDance.Position 属性类型不正确
    - TintRows.HeartTransition 属性缺失
    - TintRows.Rooms 属性默认值不正确
    - WindowResize.Pivot 属性类型不正确
    - FloatingText.Id 属性补充
    - AdvanceText.Duration 属性类型不正确
- 添加 PaletteColorWithAlpha 类型，并同步至相关事件模型
- 修改 RDCharacter 默认值的行为
- 移除坐标转换方法

### 20260306 v1.3.9-alpha1
- 修复移除事件时事件的节拍失效的异常
- 修复以下事件模型的异常
    - SetCrotchetsPerBar.CrotchetsPerBar 下限调整为 1
    - MoveCamera.Angle 属性类型不正确
    - AddOneshotBeat 部分属性未参与序列化
- 重写 BeatCalculator
- RDCharacter 添加判等方法
- 修复 RDBeat 缓存值标记错误异常
- 修复找不到文件夹资源的异常
- 序列化逻辑优化

### 20260226 v1.3.8
- 修复 Row 和 Decoration 序列化逻辑
- FileReference 添加判等方法
- 添加部分类型的集合表达式声明方式

### 20260208 v1.3.7
- 修复以下事件模型的异常
    - NewWindowDance.Tab 默认为 Actions 而不是 Windows
    - WindowResize.Tab 默认为 Actions 而不是 Windows
- 添加 LevelWriteSettings.EnableUnsafeRelaxedJsonEscaping 方法，用于启用或禁用 html 非安全字符转义
- 修复 RDBeat 比较时缓存失效异常
- 修复 RDRange 范围异常
- 修复 TimesExecuted 条件的序列化异常
- 修复 RDLevel.Add 重载异常

### 20260128 v1.3.6
- 所有事件类型都更新为记录类型
- 修复以下事件模型的异常
    - PaintHands 部分属性不能禁用
    - Tint 部分属性不能禁用
    - TintRows 部分属性不能禁用
    - MoveCamera.Window 属性缺失
- 优化源生成器逻辑

### 20260120 v1.3.5
- 适配游戏版本 v1.0.4
- 修复以下事件模型的异常
    - AddOneshotBeat 部分属性默认值不正确
    - NewWindowDance.Tab 属性默认值异常
    - ChangeCharacter 部分属性序列化异常
- 修复 RDColor 颜色读取异常
- 部分属性替换为富文本类型

### 20260108 v1.3.4
- 修复以下事件模型的异常
    - AddClassicBeat.Sound 默认值
- 修改 Condition 使用方式
- RDRoom.Default 默认值改为属性
- 添加属性 PaletteColor.PaletteIndex
- 修复枚举常量命名问题

### 20251227 v1.3.3
- 修复以下事件模型的异常
    - NarrateRowInfo.Pattern 属性类型变更
    - AddClassicBeat.Length 属性类型变更
    - ReorderSprite 缺失以下属性
        - LayerType
- 修复 Row/Decoration 事件在加载关卡时丢失其父对象引用的异常
- 将所有枚举类型移动到了类型外部

### 20251215 v1.3.2
- 修复以下事件模型的异常
    - NewWindowDance 无法自定义 Tab 属性
    - SetRowXs 缺失以下属性
        - SyncoPlayModifierOffSound
        - SyncoPitch
    - WindowResize 缺失以下属性
        - ZoomMode
    - BaseWindowEvent 限制 Y <= 3
    - SetGameSound.Tab 属性值错误
- 取消 Row 事件的类型限制，Row 事件现在可以自由放在 Classic 和 Oneshot 类型 Row 上了

### 20251210 v1.3.1
- 修复以下事件模型的异常
    - DesktopColor 继承关系错误
    - ReorderWindows.Order 属性名不正确
    - SpinningRows.Tab 属性值不正确
- 修复窗口事件的继承与部分属性

### 20251209 v1.3.0
- 适配正式版所有事件
- 修复 BeatCalculator 计算异常
- PatternCollection 添加 Length 属性
- Condition 添加隐式创建方法