## [返回](../RadiationTherapy.md)  
  
# 示例  

### 读取和写入关卡  
- [`RDLevel.LoadFile()`](class/RDLevel.md#rdlevel-loadfilestring-filepath)
- [`RDLevel.SaveFile()`](class/RDLevel.md#savefilestring-filepath)
- [`LevelInputSettings`](class/LevelInputSettings.md)
- [`LevelOutputSettings`](class/LevelOutputSettings.md)  
  
```CS  
using RhythmBase.LevelElements;  
  
//读取关卡文件 (配置是可选的)  
RDLevel rdlevel = RDLevel.LoadFile(@"Your\level.rdzip");  
  
//写入关卡文件 (配置是可选的)  
rdlevel.SaveFile(@"Your\level_copy.rdlevel");  
```  
  
**以下出现的 `rdlevel` 皆为 `RDLevel` 类型，读取和写入过程已省略。**  
  
---  
  

### 筛选事件  
- [`OrderedEventCollection<BaseEvent>.Where(Func<T, bool>)`](class/OrderedEventCollection.md#ienumerablet-wherefunct-bool-predicate)  
- [`OrderedEventCollection<BaseEvent>.Where(Func<T, bool>, float, float)`](class/OrderedEventCollection.md#ienumerablet-wherefunct-bool-predicate-float-startbeat-float-endbeat)  
- [`OrderedEventCollection<BaseEvent>.Where(Func<T, bool>, RDBeat, RDBeat)`](class/OrderedEventCollection.md#ienumerablet-wherefunct-bool-predicate-rdbeat-startbeat-rdbeat-endbeat)  
- [`OrderedEventCollection<BaseEvent>.Where(Func<T, bool>, RDRange)`](class/OrderedEventCollection.md#ienumerablet-wherefunct-bool-predicate-rdrange-range)  
- [`OrderedEventCollection<BaseEvent>.Where(Func<T, bool>, Range)`](class/OrderedEventCollection.md#ienumerablet-wherefunct-bool-predicate-range-range)  
- [`OrderedEventCollection<BaseEvent>.Where<U>(Func<U, bool>)`](class/OrderedEventCollection.md#ienumerablet-whereufunct-bool-predicate)  
- [`OrderedEventCollection<BaseEvent>.Where<U>(Func<U, bool>, float, float)`](class/OrderedEventCollection.md#ienumerablet-whereufunct-bool-predicate-float-startbeat-float-endbeat)  
- [`OrderedEventCollection<BaseEvent>.Where<U>(Func<U, bool>, RDBeat, RDBeat)`](class/OrderedEventCollection.md#ienumerablet-whereufunct-bool-predicate-rdbeat-startbeat-rdbeat-endbeat)  
- [`OrderedEventCollection<BaseEvent>.Where<U>(Func<U, bool>, RDRange)`](class/OrderedEventCollection.md#ienumerablet-whereufunct-bool-predicate-rdrange-range)  
- [`OrderedEventCollection<BaseEvent>.Where<U>(Func<U, bool>, Range)`](class/OrderedEventCollection.md#ienumerablet-whereufunct-bool-predicate-range-range)  
- [`BaseEvent.Beat`](class/BaseEvent.md#rdbeat-beat)  
- [`BaseEvent.Type`](class/BaseEvent.md#eventtype-type)  
- [`RDBeat`](class/RDBeat.md)  
- [`RDRange`](class/RDRange.md)

  
```CS  
using RhythmBase.LevelElements;  
using RhythmBase.Components;  
using RhythmBase.Events;  
  
List<BaseBeat> beats;

//筛选同时满足以下所有条件的事件:
//继承自 BaseBeat，即为节拍事件;
//Active 为 true，即已激活事件;
//小节范围在第 2 小节（包含）到倒数第 4 小节（包含）的所有事件。  

//以下筛选方法皆等效。

beats = rdlevel.Where(i =>
		i.Active &&
		2 <= i.Beat.BarBeat.bar &&
		i.Beat.BarBeat.bar < rdlevel.Last().Beat.BarBeat.bar - 3)
		.OfType<BaseBeat>()
		.ToList();


beats = rdlevel.Where(i =>
		i.Type == EventType.MoveRow &&
		i.Active &&
        rdlevel.Calculator.BeatOf(2, 1) <= i.Beat &&
        i.Beat < rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1))
		.OfType<BaseBeat>()
		.ToList();


beats = rdlevel.Where(i =>
		i.Active,
		rdlevel.Calculator.BeatOf(2, 1).BeatOnly,
		rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1).BeatOnly)
		.OfType<BaseBeat>()
		.ToList();


beats = rdlevel.Where(i =>
		i.Active,
		rdlevel.Calculator.BeatOf(2, 1),
		rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1))
        .OfType<BaseBeat>()
        .ToList();


beats = rdlevel.Where(i =>
		i.Active,
		new RDRange(rdlevel.Calculator.BeatOf(2, 1), rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1)))
        .OfType<BaseBeat>()
        .ToList();


beats = rdlevel.Where(i =>
		i.Active,
		2..^4)
        .OfType<BaseBeat>()
        .ToList();


beats = rdlevel.Where<BaseBeat>(i =>
		i.Active &&
		2 <= i.Beat.BarBeat.bar &&
		i.Beat.BarBeat.bar < rdlevel.Last().Beat.BarBeat.bar - 3)
		.ToList();


beats = rdlevel.Where<BaseBeat>(i =>
		i.Active &&
		rdlevel.Calculator.BeatOf(2, 1) <= i.Beat &&
		i.Beat < rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1))
		.ToList();


beats = rdlevel.Where<BaseBeat>(i =>
		i.Active,
		rdlevel.Calculator.BeatOf(2, 1).BeatOnly,
		rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1).BeatOnly)
		.ToList();


beats = rdlevel.Where<BaseBeat>(i =>
		i.Active,
		rdlevel.Calculator.BeatOf(2, 1),
		rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1))
		.ToList();


beats = rdlevel.Where<BaseBeat>(i =>
		i.Active,
		new RDRange(rdlevel.Calculator.BeatOf(2, 1), rdlevel.Calculator.BeatOf(rdlevel.Last().Beat.BarBeat.bar - 3, 1)))
		.ToList();


beats = rdlevel.Where<BaseBeat>(i =>
		i.Active,
		2..^4)
		.ToList();
```  
  
---  
  

### 移除所有位于动作栏的未激活事件  
- [`OrderedEventCollection<BaseEvent>.RemoveAll()`](class/OrderedEventCollection.md#int-removeallpredicatet-predicate)  
  
```CS  
using RhythmBase.LevelElements;  
using RhythmBase.Events;  
  
//筛选并移除满足位于动作栏(i.Tab == Tabs.Actions)且未激活(!i.Active)的事件：  
rdlevel.RemoveAll(i => i.Tab == Tabs.Actions && !i.Active);  
```  
  
---  
  

### 批量添加并初始化装饰  
- [`Decoration.CreateChildren()`](class/Decoration.md#t--basedecorationaction-new-createchildrentbaseevent-item)
- [`Sprite.LoadFile()`](class/Sprite.md#sprite-loadfilestring-path)
- [`Rooms.Rooms()`](class/Rooms.md#newparam-byte-rooms)  
  
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
    decoration.CreateChildren(new SetVisible() { Beat = rdlevel.Calculator.BeatOf(1), Visible = true });  
```  
  
---  
  

### 在七拍子的每一拍按键  
- [`OrderedEventCollection<BaseEvent>.Where()`](class/OrderedEventCollection.md#u-lastu-where-u--t)
- [`BaseEvent.Active`](class/BaseEvent.md#bool-active)）  
- [`BaseBeat.Clone()`](class/BaseEvent.md#t-clonet-where-t--baseevent)
- `AddClassicBeat.Split()`
- `AddFreetime.Pulse`
  
```CS  
//构造一个列表以存储拆分后的七拍  
List<BaseBeat> beats = [];

//将所有七拍拆分后的自由拍副本存储到列表  
foreach (AddClassicBeat beat in rdlevel.Where<AddClassicBeat>())
{
	//存储拆分后的自由拍
	beats.AddRange(beat.Split());

	//使原来的七拍不被激活
	beat.Active = false;
}

//在关卡内以每个自由拍的基础属性创建自由拍头  
foreach (BaseBeat beat in beats)
{
	//克隆拆分的七拍子为新的自由拍头
	AddFreeTimeBeat hit = beat.Clone<AddFreeTimeBeat>();
    
	//让自由拍头成为第七拍（按拍）
	hit.Pulse = 7;

	/*  
	以上两句等效于  
  
	AddFreeTimeBeat hit = new() {  
		Beat = beat.Beat,   
		Y = beat.Y,   
		If = beat.If,   
		Tag = beat.Tag,   
		Active = beat.Active,   
		Pulse = 7  
	};  
	beat.Parent.Add(hit);  
	*/

}
```  
  
---  
  

### 以第一个结束事件的时刻计算关卡时长  
- [`OrderedEventCollection<BaseEvent>.First()`](class/OrderedEventCollection.md#t-firstu-where-u--t)
- [`RDBeat.TimeSpan`](class/RDBeat.md#timespan-timespan)
- [`BeatCalculator.BeatOnly_Time()`](class/BeatCalculator.md#timespan-beatonly_timefloat-beat)  
  
```CS  
using RhythmBase.LevelElements;  
using RhythmBase.Events;  
using RhythmBase.Utils;  
  
//获得第一个 FinishLevel 事件  
FinishLevel finish = rdlevel.First<FinishLevel>();  
  
//显式使用节拍计算器类求得时间  
float result1 = (float)rdlevel.Calculator.BeatOnly_Time(finish.Beat.BeatOnly).TotalSeconds;  
  
//内部隐式使用节拍计算器类求得时间
float result2 = (float)finishLevel.Beat.TimeSpan.TotalSeconds;  
```  
  
---  
  

### 构造自定义事件  
- [`OrderedEventCollection<BaseEvent>.Add()`](class/OrderedEventCollection.md#addt-item)
- [`CustomEvent`](class/CustomEvent.md)
- [`INumOrExp`](interface/INumOrExp.md)
- [`NumOrExpPair`](class/NumOrExpPair.md)）    
  
在使用自定义事件时请确保游戏能够正常读取。    
  
```CS  
using Newtonsoft.Json.Linq;  
using RhythmBase.Events;  
using RhythmBase.Components;  
using RhythmBase.Components.Ease;  

**static**  
  
//创建 MyEvent 类型  
//  继承自 CustomEvent 类型  
public class MyEvent : CustomEvent  
{  
    //重写属性  
    public override Rooms Rooms => Rooms.Default;  
    public override Tabs Tab => Tabs.Actions;  
  
    //实现的属性都需要和 CustomEvent.Data 字段内的数据绑定和判空。  
  
    //实现一个 NumOrExpPair 类型属性  
    public NumOrExpPair? NumOrExpPairProperty  
    {  
        get  
        {  
            //在 Data 字段内获取所需要的内容并判空  
            var value = Data["myProperty"];  
            return value.ToObject<NumOrExpPair>() ??  
                new NumOrExpPair(  
                    value[0]?.ToObject<string>(),  
                    value[1]?.ToObject<string>()  
                );  
        }  
        set  
        {  
            //将内容保存在 Data 字段内  
            Data["myProperty"] =   
                value.HasValue ?  
                new JArray(  
                    value.Value.X.Serialize(),  
                    value.Value.Y.Serialize()) :  
                null;  
        }  
    }  
  
    //在构造函数内初始化类型  
    public MyEvent()  
    {  
        //type 字段的内容会返回到 RealType 属性上。  
        Data["type"] = nameof(MyEvent);  
    }  
}  
  
MyEvent myEvent = new();  
  
level.Add(myEvent);  
  
myEvent.Beat = rdlevel.Calculator.BeatOf(8);  
  
Console.WriteLine(myEvent.Type);        //CustomEvent  
Console.WriteLine(myEvent.RealType);    //MyEvent  
  
```  
  
在 **RhythmBase.Addition**, **RhythmHospital**, **BeatsViewer** 等项目浏览更多示例。  