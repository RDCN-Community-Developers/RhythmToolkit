## [返回](../RadiationTherapy.md)

# 示例

### 读取和写入关卡

```CS
using RhythmBase.LevelElements;
using System.IO;

//读取关卡文件 (配置是可选的)
RDLevel rdlevel = RDLevel.LoadFile(@"Your\level.rdzip");

//写入关卡文件 (配置是可选的)
rdlevel.SaveFile(@"Your\level_copy.rdlevel");
```

### 移除所有位于动作栏的未激活事件

```CS
using RhythmBase.LevelElements;
using RhythmBase.Events;

rdlevel.RemoveAll(i => i.Tab == Tabs.Actions && !i.Active);
```

### 批量添加并初始化装饰

```CS
using RhythmBase.LevelElements;
using RhythmBase.Components;
using RhythmBase.Assets;
using RhythmBase.Events;
using System.Collections.Generic;

//构造一个列表以存储装饰
List<Decoration> decorations = [];

//构造一个对素材文件的引用
Sprite sprite = Sprite.LoadFile(@"Your\Asset.json");

//以此素材构造 7 个装饰并存入列表，初始化为隐藏
for (int i = 0; i < 7; i++)
    decorations.Add(rdlevel.CreateDecoration(new Rooms(0), sprite, i, false));

//为每个装饰的最开头添加一个精灵可见性事件
foreach (Decoration decoration in decorations)
    decoration.CreateChildren<SetVisible>(new SetVisible() { BeatOnly = 1, Visible = true });
```

### 在七拍子的每一拍按键

```CS
using RhythmBase.LevelElements;
using RhythmBase.Events;
using System.Collections.Generic;

//构造一个列表以存储拆分后的七拍
List<BaseBeat> beats = [];

//将所有七拍拆分后的自由拍副本存储到列表
foreach(AddClassicBeat beat in rdlevel.Where<AddClassicBeat>())
{
    beats.AddRange(beat.Split());
    beat.Active = false;
}

//在关卡内以每个自由拍的基础属性创建自由拍头
foreach(BaseBeat beat in beats)
{
    AddFreeTimeBeat hit = beat.Clone<AddFreeTimeBeat>();
    hit.Pulse = 7;
    /*
    以上两句等效于

    AddFreeTimeBeat hit = new() {
        BeatOnly=beat.BeatOnly, 
        Y = beat.Y, 
        If = beat.If, 
        Tag = beat.Tag, 
        Active = beat.Active, 
        Pulse = 7
    };
    beat.ParentLevel.Add(hit);

    */

}
```

### 以第一个结束事件的时刻计算关卡时长
```CS
using RhythmBase.LevelElements;
using RhythmBase.Events;
using RhythmBase.Utils;

//获得第一个 FinishLevel 事件。
FinishLevel finishLevel = rdlevel.First<FinishLevel>();
//以关卡构建节拍计算器类
BeatCalculator calculator = new(rdlevel);

//显式使用节拍计算器类求得时间
float result1 = (float)calculator.BeatOnly_Time(finishLevel.BeatOnly).TotalSeconds;

//内部隐式使用节拍计算器类求得时间（仅用于临时调用）
float result2 = (float)finishLevel.Time.TotalSeconds;
```

在 **RhythmBase.Addition**, **RhythmHospital**, **BeatsViewer** 等项目浏览更多示例。