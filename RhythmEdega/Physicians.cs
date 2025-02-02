//using RhythmBase.Components;
//using RhythmBase.Events;
//using RhythmBase.LevelElements;
//using RhythmBase.Extensions;
//using RhythmBase.Utils;
//namespace Physicians
//{
//    public static class Edega
//    {
//        static readonly List<LegalBeat> LegalList = new();
//        static string name = "";
//        static RDBeatCalculator Calculator;
//        class LegalBeat
//        {
//            public RDBaseBeat? beat;
//            public HashSet<(Warn, string)> warnInfo = new();
//            public TimeSpan time;
//        }
//        class BeatAudio
//        {
//            public RDAudio? Audio;
//            public LegalBeat? Parent;
//            public float Beat;
//            public BeatRangeType Type;
//        }
//        enum BeatRangeType
//        {
//            None,
//            In,
//            Out,
//        }
//        enum Warn
//        {
//            Safe = 0b0,
//            Intention = 0b1,
//            Warning = 0b11,
//            Illegal = 0b111
//        }
//        static void DetectingShakeritis(string name)
//        {
//            foreach (LegalBeat item1 in LegalList)
//            {
//                foreach (LegalBeat item2 in LegalList.TakeLast(LegalList.Count - LegalList.IndexOf(item1) - 1))
//                {
//                    if (item1.beat?.Beat < item2.beat?.Beat &&
//                        item2.beat.Beat < item1.beat.Beat + item1.beat.Length)
//                    {
//                        if (SimilarSoundEffect(item1.beat.BeatSound.Filename, item2.beat.BeatSound.Filename))
//                        {
//                            if (item1.beat.BeatSound.Filename == item2.beat.BeatSound.Filename &&
//                                item1.beat.BeatSound.Pan == item2.beat.BeatSound.Pan &&
//                                item1.beat.BeatSound.Pitch == item2.beat.BeatSound.Pitch)
//                            {
//                                item1.warnInfo.Add((Warn.Illegal, $"{name}: {item1.beat.BeatSound.Filename}"));
//                                item2.warnInfo.Add((Warn.Illegal, $"{name}: {item1.beat.BeatSound.Filename}"));
//                            }
//                            else
//                            {
//                                item1.warnInfo.Add((Warn.Warning, $"{name}: {item1.beat.BeatSound.Filename}, Pitch:{item1.beat.BeatSound.Pitch}, Pan:{item1.beat.BeatSound.Pan}"));
//                                item1.warnInfo.Add((Warn.Warning, $"{name}: {item1.beat.BeatSound.Filename}, Pitch:{item1.beat.BeatSound.Pitch}, Pan:{item1.beat.BeatSound.Pan}"));
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        static bool SimilarSoundEffect(string name1, string name2)
//        {
//            List<string[]> SimilarSoundEffectName = new()
//            {
//                new string[]{ "Shaker", "Shaker High"},//沙锤
//		        new string[]{ "Claves High", "Claves Low"},//击木
//		        new string[]{ "Wood Block High", "Wood Block Low"},//木块
//                new string[]{ "Sizzle", "ClosedHat"},
//                new string[]{ "Kick", "KickEcho", "Chuck"},
//            };
//            return (name1 == name2) || (SimilarSoundEffectName.FirstOrDefault(i => i.Contains(name1))?.Contains(name2) ?? false);
//        }
//        //static void DetectingPseudos(string name)
//        //{
//        //    List<(LegalBeat beat, Hit hit)> pulseList = LegalList
//        //        .SelectMany(i => i.beat.HitTimes()
//        //            .Select(j => (i, j)))
//        //        .OrderBy(i => i.j.BeatOnly)
//        //        .ToList();
//        //    for (int i = 0; i < pulseList.Count - 1; i++)
//        //    {
//        //        double interval = Math.Abs(Calculator.IntervalTime(pulseList[i].hit.BeatOnly, pulseList[i + 1].hit.BeatOnly).TotalMilliseconds);
//        //        if (10 < interval && interval < 100)
//        //        {
//        //            pulseList[i].beat.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //            pulseList[i + 1].beat.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //        }
//        //    }
//        //    //foreach (var item1 in pulseList)
//        //    //{
//        //    //    foreach (var item2 in pulseList.TakeLast(LegalList.Count - LegalList.IndexOf(item1.i) - 1))
//        //    //    {
//        //    //        double interval = Math.Abs(Calculator.Interval_Time(item1.j.BeatOnly, item2.j.BeatOnly).TotalMilliseconds);
//        //    //        if (10 < interval && interval < 100)
//        //    //        {
//        //    //            item1.i.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //    //            item2.i.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //    //        }
//        //    //    }
//        //    //}
//        //}
//        static void DetectingHeckSwing(string name)
//        {
//            foreach (var item in LegalList.Where(i => i.beat.Type == RDEventType.AddClassicBeat))
//            {
//                if ((item.beat as RDAddClassicBeat).Swing == 1)
//                {
//                    item.warnInfo.Add((Warn.Illegal, $"{name}"));
//                }
//            }
//        }
//        //static void DetectingShortHold(string name)
//        //{
//        //    foreach (var item in LegalList.Where(i => i.beat.Type == EventType.AddClassicBeat ||
//        //                                              i.beat.Type == EventType.AddFreeTimeBeat ||
//        //                                              i.beat.Type == EventType.PulseFreeTimeBeat))
//        //    {
//        //        double interval, max = 100;
//        //        switch (item.beat.Type)
//        //        {
//        //            case EventType.AddClassicBeat:
//        //                AddClassicBeat temp1 = (AddClassicBeat)item.beat;
//        //                interval = Math.Abs(Calculator.IntervalTime(temp1.BeatOnly + temp1.Length, temp1.BeatOnly + temp1.Length + temp1.Hold).TotalMilliseconds);
//        //                if (temp1.Hitable && temp1.Hold > 0 && interval < max)
//        //                {
//        //                    item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //                }
//        //                break;
//        //            case EventType.AddFreeTimeBeat:
//        //                AddFreeTimeBeat temp2 = (AddFreeTimeBeat)item.beat;
//        //                interval = Math.Abs(Calculator.IntervalTime(temp2.BeatOnly + temp2.Length, temp2.BeatOnly + temp2.Length + temp2.Hold).TotalMilliseconds);
//        //                if (temp2.Hitable && temp2.Hold > 0 && interval < max)
//        //                {
//        //                    item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //                }
//        //                break;
//        //            case EventType.PulseFreeTimeBeat:
//        //                var temp3 = (PulseFreeTimeBeat)item.beat;
//        //                interval = Math.Abs(Calculator.IntervalTime(temp3.BeatOnly + temp3.Length, temp3.BeatOnly + temp3.Length + temp3.Hold).TotalMilliseconds);
//        //                if (temp3.Hitable && temp3.Hold > 0 && interval < max)
//        //                {
//        //                    item.warnInfo.Add((Warn.Warning, $"{name}: {interval}ms"));
//        //                }
//        //                break;
//        //            default:
//        //                break;
//        //        }
//        //    }
//        //}
//        static void DetectingBadRowXs(string name)
//        {
//            foreach (var item in LegalList.Where(i => i.beat!.Type == RDEventType.AddClassicBeat))  // 对于每一个七拍
//            {
//                var beat = item.beat as RDAddClassicBeat;
//                if (beat!.RowXs[0] == Patterns.X) // 如果首拍带 x
//                    item.warnInfo.Add((Warn.Illegal, $"{name}: {RDSetRowXs.GetPatternString(beat.RowXs)}"));
//                else if (beat.RowXs.Count(i => i == Patterns.X) >= 4 && // 如果 x 个数 >=4
//                    RDSetRowXs.GetPatternString(beat.RowXs) != "-xx-xx") // 且不是标准三拍子
//                    item.warnInfo.Add((Warn.Illegal, $"{name}: {RDSetRowXs.GetPatternString(beat.RowXs)}"));
//            }
//        }
//        public static void DetectingAll(RDLevel level)
//        {
//            var now = DateTime.Now;
//            Calculator = new(level);
//            //读取结束
//            Console.WriteLine($"Loading successful.");
//            Console.WriteLine($"{level.Count} events loaded.");
//            //获取所有节拍事件，初始化标记为空
//            foreach (RDBaseBeat item in level.Where<RDBaseBeat>())
//            {
//                if (item.Active &&
//                    (item.Type == RDEventType.AddOneshotBeat ||
//                    item.Type == RDEventType.AddClassicBeat ||
//                    item.Type == RDEventType.AddFreeTimeBeat ||
//                    item.Type == RDEventType.PulseFreeTimeBeat) &&
//                    item.Player != RDPlayerType.CPU)
//                {
//                    LegalList.Add(new LegalBeat { beat = item });
//                }
//            }
//            Console.WriteLine($"Loaded. {(DateTime.Now - now).TotalMilliseconds}ms");
//            now = DateTime.Now;
//            //获取结束
//            Console.WriteLine($"{LegalList.Count} beats event loaded.");
//            Console.ForegroundColor = ConsoleColor.Blue;
//            now = DateTime.Now;
//            //同声共奏检测
//            name = "Shakeritis";
//            Console.WriteLine($"Detecting {name}...");
//            DetectingShakeritis(name);
//            Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
//            now = DateTime.Now;
//            ////伪双押检测
//            //name = "Pseudos";
//            //Console.WriteLine($"Detecting {name}...");
//            //DetectingPseudos(name);
//            //Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
//            //now = DateTime.Now;
//            //幽灵摇摆检测
//            name = "Heck Swing";
//            Console.WriteLine($"Detecting {name}...");
//            DetectingHeckSwing(name);
//            Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
//            now = DateTime.Now;
//            ////长按过短检测
//            //name = "Short hold";
//            //Console.WriteLine($"Detecting {name}...");
//            //DetectingShortHold(name);
//            //Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
//            //now = DateTime.Now;
//            //设置X型错误
//            name = "Bad row Xs";
//            Console.WriteLine($"Detecting {name}...");
//            DetectingBadRowXs(name);
//            Console.WriteLine($"Done. {(DateTime.Now - now).TotalMilliseconds}ms");
//            now = DateTime.Now;
//            Console.ResetColor();
//            Console.WriteLine(name);
//            //打印所有问题
//            if (LegalList.Count > 0)
//            {
//                foreach (LegalBeat item1 in LegalList.Where(i => i.warnInfo.Any()))
//                {
//                    ConsoleColor color;
//                    Console.WriteLine($"{item1.beat?.Beat.BarBeat}");
//                    Console.WriteLine($"{item1.beat}");
//                    foreach (var info in item1.warnInfo)
//                    {
//                        color = info.Item1 switch
//                        {
//                            Warn.Intention => ConsoleColor.Yellow,
//                            Warn.Warning => ConsoleColor.DarkYellow,
//                            Warn.Illegal => ConsoleColor.Red,
//                            _ => ConsoleColor.White,
//                        };
//                        Console.ForegroundColor = color;
//                        Console.WriteLine($"{(info.Item2)}");
//                        Console.ResetColor();
//                    }
//                    Console.WriteLine();
//                }
//            }
//            else
//            {
//                Console.WriteLine("Nothing detected.");
//            }
//        }
//    }
//    public class Ian
//    {
//        private readonly RDLevel level;
//        private readonly RDBeatCalculator calculator;
//        public Ian(RDLevel level)
//        {
//            this.level = level;
//            calculator = new RDBeatCalculator(level);
//        }
//        public void SplitRDGSG()
//        {
//            List<SayReadyGetSetGo> Adds = new();
//            foreach (SayReadyGetSetGo item in level.Where<SayReadyGetSetGo>())
//            {
//                if (item.Splitable)
//                {
//                    Adds.AddRange(item.Split());
//                    item.Active = false;
//                }
//            }
//            level.AddRange(Adds);
//        }
//        public void SplitClassicBeat()
//        {
//            List<RDBaseEvent> events = new();
//            foreach (RDAddClassicBeat item in level.Where<RDAddClassicBeat>())
//            {
//                events.AddRange(item.Split());
//                item.Active = false;
//            }
//            level.AddRange(events);
//        }
//        public void PressOnEveryBeat()
//        {
//            List<RDBaseBeat> Beats = new();
//            List<RDAddFreeTimeBeat> FreeTimes = new();
//            foreach (RDAddClassicBeat item in level.Where<RDAddClassicBeat>())
//            {
//                Beats.AddRange(item.Split());
//                item.Active = false;
//            }
//            foreach (RDBaseBeat item in Beats)
//            {
//                var n = item.Clone<RDAddFreeTimeBeat>();
//                n.Pulse = 6;
//                FreeTimes.Add(n);
//            }
//            level.AddRange(FreeTimes);
//        }
//        public void DisposeTags()
//        {
//            List<RDBaseEvent> Events = new();
//            foreach (RDTagAction item in level.Where<RDTagAction>())
//            {
//                var EventGroup = level.GetTaggedEvents(item.ActionTag, item.Action != RDTagAction.Actions.Run);
//                foreach (var Group in EventGroup)
//                {
//                    float StartBeat = Group.First().Beat.BeatOnly;
//                    var CopiedGroup = Group.Select(i => Utils.Clone(i)).ToList();
//                    foreach (RDBaseEvent Copy in CopiedGroup)
//                    {
//                        Copy.Beat += (item.Beat - StartBeat);
//                        Copy.Tag = "";
//                        Events.Add(Copy);
//                    }
//                    item.Active = false;
//                }
//            }
//            level.AddRange(Events);
//        }
//        //public IEnumerable<(Hit,Hit,TimeSpan)> GetShortestHitInterval()
//        //{
//        //    List<Hit> hits = new();
//        //    List<(Hit,Hit,TimeSpan)> interval = new();
//        //    foreach (Row row in level.Rows)
//        //        hits.AddRange(row.HitBeats());
//        //    hits = hits
//        //            .GroupBy(i => i.BeatOnly)
//        //            .Select(i => i.First())
//        //            .OrderBy(i => i.BeatOnly)
//        //            .ToList();
//        //    for (int i = 0; i < hits.Count - 1; i++)
//        //        interval.Add((
//        //            hits[i],
//        //            hits[i + 1],
//        //            calculator.BeatOnly_Time(hits[i + 1].BeatOnly + hits[i + 1].Hold)
//        //        ));
//        //    var min = interval.Min(i => i.Item3);
//        //    return interval.Where(i => i.Item3 == min);
//        //}
//    }
//}
