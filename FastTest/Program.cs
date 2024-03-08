using RhythmBase.Events;
using RhythmBase.Objects;
using RhythmBase.Util;
using System.Runtime.InteropServices;

//读取文件
Console.WriteLine("Input filepath:");
Console.WriteLine("E:\\Resources\\你我皆相连2\\Connected\\level.rdlevel");
var now = DateTime.Now;
Console.WriteLine("Initializing...");
RDLevel level = RDLevel.LoadFile(new FileInfo("E:\\Resources\\你我皆相连2\\Connected\\level.rdlevel"));//Console.ReadLine() ?? ""));
BeatCalculator Calculator = new(level);

//读取结束
Console.WriteLine($"Loading successful.");
Console.WriteLine($"{level.Count} events loaded.");

//排序
level.Sort();

//获取所有节拍事件，初始化标记为空
List<LegalBeat> LegalList = new List<LegalBeat>();
foreach(BaseBeat item in level.Where<BaseBeat>())
{
	if (item.Type == EventType.AddOneshotBeat ||
		item.Type == EventType.AddClassicBeat ||
		item.Type == EventType.AddFreeTimeBeat ||
		item.Type == EventType.PulseFreeTimeBeat) 
	{
        LegalList.Add(new LegalBeat { beat = item });// ,time=item.Time});
	}
}

Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
now = DateTime.Now;

//获取结束
Console.WriteLine($"{LegalList.Count} beats event loaded.");

string name;
Console.ForegroundColor = ConsoleColor.Blue;


//同声共奏检测
name = "Shakeritis";
Console.WriteLine($"Detecting {name}...");
DetectingShakeritis();
Console.WriteLine($"Done. {(DateTime.Now-now).TotalMilliseconds}ms");
now = DateTime.Now;

//伪双押检测
name = "Pseudos";
Console.WriteLine($"Detecting {name}...");
DetectingPseudos();
Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
now = DateTime.Now;

//幽灵摇摆检测
name = "Heck Swing";
Console.WriteLine($"Detecting {name}...");
DetectingHeckSwing();
Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
now = DateTime.Now;

//长按过短检测
name = "Short hold";
Console.WriteLine($"Detecting {name}...");
DetectingShortHold();
Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
now = DateTime.Now;

//设置X型错误
name = "Bad row Xs";
Console.WriteLine($"Detecting {name}...");
DetectingBadRowXs();
Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
now = DateTime.Now;

Console.ResetColor();
Console.WriteLine();
//打印所有问题
foreach (LegalBeat item1 in LegalList.Where(i => i.warnInfo.Any())){
    ConsoleColor color;
	Console.WriteLine($"{item1.beat.BarBeat}");
	Console.WriteLine($"{item1.beat}");
	foreach (var info in item1.warnInfo)
	{
        switch (info.Item1)
		{
			case Warn.Intention:
				color = ConsoleColor.Yellow;
				break;
			case Warn.Warning:
				color = ConsoleColor.DarkYellow;
				break;
			case Warn.Illegal:
				color = ConsoleColor.Red;
				break;
			default:
				color = ConsoleColor.White;
				break;
		}
		Console.ForegroundColor = color;
		Console.WriteLine($"{(info.Item2)}");
		Console.ResetColor();
	}
	Console.WriteLine();
}

//level.SaveFile(new FileInfo("E:\\Resources\\你我皆相连2\\Connected\\level2.rdlevel"));//Console.ReadLine() ?? string.Empty));

void DetectingShakeritis()
{
    foreach (LegalBeat item1 in LegalList)
    {
        foreach (LegalBeat item2 in LegalList.TakeLast(LegalList.Count - LegalList.IndexOf(item1) - 1))
        {
            if (item1.beat.BeatOnly < item2.beat.BeatOnly &&
                item2.beat.BeatOnly < item1.beat.BeatOnly + item1.beat.Length) 
            {
                if (item1.beat.BeatSound.Filename == item2.beat.BeatSound.Filename)
                {
                    if (item1.beat.BeatSound.Pan == item2.beat.BeatSound.Pan &&
                        item1.beat.BeatSound.Pitch == item2.beat.BeatSound.Pitch)
                    {
                        item1.warnInfo.Add((Warn.Illegal, $"{name}: {item1.beat.BeatSound.Filename}"));
                        item2.warnInfo.Add((Warn.Illegal, $"{name}: {item1.beat.BeatSound.Filename}"));
                    }
                    else
                    {
                        item1.warnInfo.Add((Warn.Warning, $"{name}: {item1.beat.BeatSound.Filename}"));
                        item1.warnInfo.Add((Warn.Warning, $"{name}: {item1.beat.BeatSound.Filename}"));
                    }
                }
            }
        }
    }
}

void DetectingPseudos() {
    List<(LegalBeat beat, Hit hit)> pulseList = LegalList
        .SelectMany(i => i.beat.HitTime()
            .Select(j => (i, j)))
        .OrderBy(i => i.j.BeatOnly)
        .ToList();
    for (int i = 0; i < pulseList.Count() - 1; i++) 
    {
        double interval = Math.Abs(Calculator.Interval_Time(pulseList[i].hit.BeatOnly, pulseList[i+1].hit.BeatOnly).TotalMilliseconds);
        if (10 < interval && interval < 100)
        {
            pulseList[i].beat.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
            pulseList[i + 1].beat.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
        }
    }
    //foreach (var item1 in pulseList)
    //{
    //    foreach (var item2 in pulseList.TakeLast(LegalList.Count - LegalList.IndexOf(item1.i) - 1))
    //    {
    //        double interval = Math.Abs(Calculator.Interval_Time(item1.j.BeatOnly, item2.j.BeatOnly).TotalMilliseconds);
    //        if (10 < interval && interval < 100)
    //        {
    //            item1.i.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
    //            item2.i.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
    //        }
    //    }
    //}
}

void DetectingHeckSwing()
{
	foreach (var item in LegalList.Where(i => i.beat.Type == EventType.AddClassicBeat)) 
    {
        if (((AddClassicBeat)item.beat).Swing == 1)
        {
            item.warnInfo.Add((Warn.Illegal, $"{name}"));
        }
    }
}

void DetectingShortHold()
{
    foreach (var item in LegalList.Where(i => i.beat.Type == EventType.AddClassicBeat ||
                                              i.beat.Type == EventType.AddFreeTimeBeat ||
                                              i.beat.Type == EventType.PulseFreeTimeBeat))
    {
        double interval, max = 100;
        switch (item.beat.Type)
        {
            case EventType.AddClassicBeat:
                AddClassicBeat temp1 = (AddClassicBeat)item.beat;
                interval = Math.Abs(Calculator.Interval_Time(temp1.BeatOnly + temp1.Length, temp1.BeatOnly + temp1.Length + temp1.Hold).TotalMilliseconds);
                if (temp1.Hitable && temp1.Hold > 0 && interval < max)
                {
                    item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
                }
                break;
            case EventType.AddFreeTimeBeat:
                AddFreeTimeBeat temp2 = (AddFreeTimeBeat)item.beat;
                interval = Math.Abs(Calculator.Interval_Time(temp2.BeatOnly + temp2.Length, temp2.BeatOnly + temp2.Length + temp2.Hold).TotalMilliseconds);

                if (temp2.Hitable && temp2.Hold > 0 && interval < max)
                {
                    item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
                }
                break;
            case EventType.PulseFreeTimeBeat:
                var temp3 = (PulseFreeTimeBeat)item.beat;
                interval = Math.Abs(Calculator.Interval_Time(temp3.BeatOnly + temp3.Length, temp3.BeatOnly + temp3.Length + temp3.Hold).TotalMilliseconds);

                if (temp3.Hitable && temp3.Hold > 0 && interval < max)
                {
                    item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
                }
                break;
            default:
                break;
        }
    }
}

void DetectingBadRowXs()
{
    foreach (var item in LegalList.Where(i => i.beat.Type == EventType.AddClassicBeat))
    {
        var beat = (AddClassicBeat)item.beat;
        if (beat.RowXs[0] == Patterns.X)
            item.warnInfo.Add((Warn.Illegal, $"{name}: {SetRowXs.GetPatternString(beat.RowXs)}"));
        else if (beat.RowXs.Count(i => i == Patterns.X) >= 4 &&
            SetRowXs.GetPatternString(beat.RowXs) != "-xx-xx")
            item.warnInfo.Add((Warn.Illegal, $"{name}: {SetRowXs.GetPatternString(beat.RowXs)}"));
    }
}

class LegalBeat{
	public BaseBeat beat;
	public HashSet<(Warn,string)> warnInfo = new();
    public TimeSpan time;
}

class BeatAudio
{
	public Audio Audio;
	public LegalBeat Parent;
	public float Beat;
	public BeatRangeType Type;
}
enum BeatRangeType
{
	None,
	In,
	Out,
}
enum Warn{
	Safe = 0b0,
	Intention = 0b1,
	Warning = 0b11,
	Illegal = 0b111
}
