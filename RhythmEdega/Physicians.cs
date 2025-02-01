using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Extensions;
namespace Physicians
{
	public static class Edega
	{
		static readonly List<BeatInfo> LegalList = [];
		static string name = "";
		public struct BeatInfo(BaseBeat beat)
		{
			public BaseBeat Beat { get; } = beat;
			public BaseBeat[] RelatedBeats { get; init; } = [];
			public string Message { get; init; } = "";
			public required Level Level { get; init; }
		}
		enum BeatRangeType
		{
			None,
			In,
			Out,
		}
		public enum Level
		{
			Safe = 0b0,
			Intention = 0b1,
			Warning = 0b11,
			Illegal = 0b111
		}
		private static string[][] similarsounds =
			[
				["Shaker", "Shaker High"],//沙锤
				["Claves High", "Claves Low"],//击木
				["Wood Block High", "Wood Block Low"],//木块
				["Sizzle", "ClosedHat"],
				["Kick", "KickEcho", "Chuck"],
			];
		public static IEnumerable<BeatInfo> DetectingShakeritis(RDLevel rdlevel)
		{
			List<BaseBeat> beatsToDetect = [.. rdlevel.Where<BaseBeat>(i => i.Active && i.BeatSound().Volume > 0)];
			while (beatsToDetect.Count > 0)
			{
				var beat = beatsToDetect[0];
				var hits = beat.HitTimes();
				var sound = beat.BeatSound();
				if (hits.Any())
				{
					List<BaseBeat> otherbeats = [];
					var after = beat.After();
					foreach (var beatafter in after.Where(i => i.Active))
					{
						var hitsafter = beatafter.HitTimes();
						var soundafter = beatafter.BeatSound();
						if (hitsafter.Any(h1 => hits.Any(h2 => (h1.Beat.TimeSpan - h2.Beat.TimeSpan).TotalMilliseconds < 1)))
						{
							if (sound.Filename == soundafter.Filename)
							{
								otherbeats.Add(beatafter);
								beatsToDetect.Remove(beatafter);
								break;
							}
							var similars = similarsounds.Where(i => i.Contains(sound.Filename));
							if (similars.Any(i => i.Contains(soundafter.Filename)))
							{
								otherbeats.Add(beatafter);
								beatsToDetect.Remove(beatafter);
							}
						}
					}
					if (otherbeats.Count != 0)
						yield return new BeatInfo(beat)
						{
							RelatedBeats = [.. otherbeats],
							Message = "Shakeritis",
							Level = Level.Warning,
						};
				}
				beatsToDetect.Remove(beat);
			}
		}
		public static IEnumerable<BeatInfo> DetectingPseudos(RDLevel rdlevel)
		{
			List<BaseBeat> beatsToDetect = [.. rdlevel.Where<BaseBeat>(i => i.Active && i.BeatSound().Volume > 0)];
			while (beatsToDetect.Count > 0)
			{
				var beat = beatsToDetect[0];
				var hits = beat.HitTimes();
				if (hits.Any())
				{
					var after = beat.After();
					foreach (var beatafter in after.Where(i => i.Active))
					{
						var hitsafter = beatafter.HitTimes();
						if (hitsafter.Any(h1 => hits.Any(h2 => (h1.Beat.TimeSpan - h2.Beat.TimeSpan).TotalMilliseconds is > 10 and < 100)))
						{
							yield return new BeatInfo(beat)
							{
								RelatedBeats = [beatafter],
								Message = "Pseudos",
								Level = Level.Illegal,
							};
						}
					}
				}
				beatsToDetect.Remove(beat);
			}
		}
		public static IEnumerable<BeatInfo> DetectingHeckSwing(RDLevel rdlevel)
		{
			List<AddClassicBeat> beatsToDetect = [.. rdlevel.Where<AddClassicBeat>(i => i.Active && i.BeatSound().Volume > 0)];
			foreach (var beat in beatsToDetect)
			{
				if (beat.Swing == 1)
				{
					yield return new(beat)
					{
						Message = "HeckSwing",
						Level = Level.Illegal
					};
				}
			}
		}
		static void DetectingShortHold(string name)
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
						interval = Math.Abs(Calculator.IntervalTime(temp1.BeatOnly + temp1.Length, temp1.BeatOnly + temp1.Length + temp1.Hold).TotalMilliseconds);
						if (temp1.Hitable && temp1.Hold > 0 && interval < max)
						{
							item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
						}
						break;
					case EventType.AddFreeTimeBeat:
						AddFreeTimeBeat temp2 = (AddFreeTimeBeat)item.beat;
						interval = Math.Abs(Calculator.IntervalTime(temp2.BeatOnly + temp2.Length, temp2.BeatOnly + temp2.Length + temp2.Hold).TotalMilliseconds);
						if (temp2.Hitable && temp2.Hold > 0 && interval < max)
						{
							item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
						}
						break;
					case EventType.PulseFreeTimeBeat:
						var temp3 = (PulseFreeTimeBeat)item.beat;
						interval = Math.Abs(Calculator.IntervalTime(temp3.BeatOnly + temp3.Length, temp3.BeatOnly + temp3.Length + temp3.Hold).TotalMilliseconds);
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
		//static void DetectingBadRowXs(string name)
		//{
		//    foreach (var item in LegalList.Where(i => i.beat!.Type == RDEventType.AddClassicBeat))  // 对于每一个七拍
		//    {
		//        var beat = item.beat as RDAddClassicBeat;
		//        if (beat!.RowXs[0] == Patterns.X) // 如果首拍带 x
		//            item.warnInfo.Add((Warn.Illegal, $"{name}: {RDSetRowXs.GetPatternString(beat.RowXs)}"));
		//        else if (beat.RowXs.Count(i => i == Patterns.X) >= 4 && // 如果 x 个数 >=4
		//            RDSetRowXs.GetPatternString(beat.RowXs) != "-xx-xx") // 且不是标准三拍子
		//            item.warnInfo.Add((Warn.Illegal, $"{name}: {RDSetRowXs.GetPatternString(beat.RowXs)}"));
		//    }
		//}
		//public static void DetectingAll(RDLevel level)
		//{
		//    var now = DateTime.Now;
		//    Calculator = new(level);
		//    //读取结束
		//    Console.WriteLine($"Loading successful.");
		//    Console.WriteLine($"{level.Count} events loaded.");
		//    //获取所有节拍事件，初始化标记为空
		//    foreach (RDBaseBeat item in level.Where<RDBaseBeat>())
		//    {
		//        if (item.Active &&
		//            (item.Type == RDEventType.AddOneshotBeat ||
		//            item.Type == RDEventType.AddClassicBeat ||
		//            item.Type == RDEventType.AddFreeTimeBeat ||
		//            item.Type == RDEventType.PulseFreeTimeBeat) &&
		//            item.Player != RDPlayerType.CPU)
		//        {
		//            LegalList.Add(new LegalBeat { beat = item });
		//        }
		//    }
		//    Console.WriteLine($"Loaded. {(DateTime.Now - now).TotalMilliseconds}ms");
		//    now = DateTime.Now;
		//    //获取结束
		//    Console.WriteLine($"{LegalList.Count} beats event loaded.");
		//    Console.ForegroundColor = ConsoleColor.Blue;
		//    now = DateTime.Now;
		//    //同声共奏检测
		//    name = "Shakeritis";
		//    Console.WriteLine($"Detecting {name}...");
		//    DetectingShakeritis(name);
		//    Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
		//    now = DateTime.Now;
		//    ////伪双押检测
		//    //name = "Pseudos";
		//    //Console.WriteLine($"Detecting {name}...");
		//    //DetectingPseudos(name);
		//    //Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
		//    //now = DateTime.Now;
		//    //幽灵摇摆检测
		//    name = "Heck Swing";
		//    Console.WriteLine($"Detecting {name}...");
		//    DetectingHeckSwing(name);
		//    Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
		//    now = DateTime.Now;
		//    ////长按过短检测
		//    //name = "Short hold";
		//    //Console.WriteLine($"Detecting {name}...");
		//    //DetectingShortHold(name);
		//    //Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
		//    //now = DateTime.Now;
		//    //设置X型错误
		//    name = "Bad row Xs";
		//    Console.WriteLine($"Detecting {name}...");
		//    DetectingBadRowXs(name);
		//    Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
		//    now = DateTime.Now;
		//    Console.ResetColor();
		//    Console.WriteLine(name);
		//    //打印所有问题
		//    if (LegalList.Count > 0)
		//    {
		//        foreach (LegalBeat item1 in LegalList.Where(i => i.warnInfo.Any()))
		//        {
		//            ConsoleColor color;
		//            Console.WriteLine($"{item1.beat?.Beat.BarBeat}");
		//            Console.WriteLine($"{item1.beat}");
		//            foreach (var info in item1.warnInfo)
		//            {
		//                color = info.Item1 switch
		//                {
		//                    Warn.Intention => ConsoleColor.Yellow,
		//                    Warn.Warning => ConsoleColor.DarkYellow,
		//                    Warn.Illegal => ConsoleColor.Red,
		//                    _ => ConsoleColor.White,
		//                };
		//                Console.ForegroundColor = color;
		//                Console.WriteLine($"{(info.Item2)}");
		//                Console.ResetColor();
		//            }
		//            Console.WriteLine();
		//        }
		//    }
		//    else
		//    {
		//        Console.WriteLine("Nothing detected.");
		//    }
	}
	public class Ian
	{
		private readonly RDLevel level;
		public Ian(RDLevel level)
		{
			this.level = level;
		}
		public void SplitRDGSG()
		{
			List<SayReadyGetSetGo> Adds = new();
			foreach (SayReadyGetSetGo item in level.Where<SayReadyGetSetGo>())
			{
				if (item.Splitable)
				{
					Adds.AddRange(item.Split());
					item.Active = false;
				}
			}
			level.AddRange(Adds);
		}
		public void SplitClassicBeat()
		{
			List<BaseEvent> events = new();
			foreach (AddClassicBeat item in level.Where<AddClassicBeat>())
			{
				events.AddRange(item.Split());
				item.Active = false;
			}
			level.AddRange(events);
		}
		public void PressOnEveryBeat()
		{
			List<BaseBeat> Beats = new();
			List<AddFreeTimeBeat> FreeTimes = new();
			foreach (AddClassicBeat item in level.Where<AddClassicBeat>())
			{
				Beats.AddRange(item.Split());
				item.Active = false;
			}
			foreach (BaseBeat item in Beats)
			{
				var n = item.Clone<AddFreeTimeBeat>();
				n.Pulse = 6;
				FreeTimes.Add(n);
			}
			level.AddRange(FreeTimes);
		}
		public void DisposeTags()
		{
			List<BaseEvent> Events = new();
			foreach (TagAction item in level.Where<TagAction>())
			{
				var EventGroup = level.GetTaggedEvents(item.ActionTag, item.Action != TagAction.Actions.Run);
				foreach (var Group in EventGroup)
				{
					float StartBeat = Group.First().Beat.BeatOnly;
					var CopiedGroup = Group.Select(i => (i)).ToList();
					foreach (BaseEvent Copy in CopiedGroup)
					{
						Copy.Beat += (item.Beat.BeatOnly - StartBeat);
						Copy.Tag = "";
						Events.Add(Copy);
					}
					item.Active = false;
				}
			}
			level.AddRange(Events);
		}
		//public IEnumerable<(Hit,Hit,TimeSpan)> GetShortestHitInterval()
		//{
		//    List<Hit> hits = new();
		//    List<(Hit,Hit,TimeSpan)> interval = new();
		//    foreach (Row row in level.Rows)
		//        hits.AddRange(row.HitBeats());
		//    hits = hits
		//            .GroupBy(i => i.BeatOnly)
		//            .Select(i => i.First())
		//            .OrderBy(i => i.BeatOnly)
		//            .ToList();
		//    for (int i = 0; i < hits.Count - 1; i++)
		//        interval.Add((
		//            hits[i],
		//            hits[i + 1],
		//            calculator.BeatOnly_Time(hits[i + 1].BeatOnly + hits[i + 1].Hold)
		//        ));
		//    var min = interval.Min(i => i.Item3);
		//    return interval.Where(i => i.Item3 == min);
		//}
	}
}
