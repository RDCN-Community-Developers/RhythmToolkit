using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Utils;

namespace RhythmBase.Test
{
	[TestClass]
	public sealed class Test
	{
		[TestMethod]
		public void AddRemoveTest()
		{
			RDLevel level = [];
			Decoration deco = [];
			Move move = new();
			deco.Add(move);
			Console.WriteLine($"{level.Count}, {deco.Count}");
			level.Decorations.Add(deco);
			Console.WriteLine($"{level.Count}, {deco.Count}");
			level.Add(move);
			Console.WriteLine($"{level.Count}, {deco.Count}");
			level.Decorations.Remove(deco);
			Console.WriteLine($"{level.Count}, {deco.Count}");
			level.Remove(move);
			Console.WriteLine($"{level.Count}, {deco.Count}");
		}
		[TestMethod]
		public void GenerateLevelWithAllEventTypes()
		{
			RDLevel level = [];
			level.Rows.Add(new() { RowType = RowTypes.Classic });
			level.Rows.Add(new() { RowType = RowTypes.Oneshot });
			level.Decorations.Add([]);
			Dictionary<Tabs, int> count = [];
			foreach (var type in ((EventType[])Enum.GetValues(typeof(EventType))).Where(i => !EventTypeUtils.CustomTypes.Contains(i)))
			{
				if (EventTypeUtils.CustomTypes.Contains(type))
					continue;
				if (type == EventType.AdvanceText)
					continue;
				var e = (IBaseEvent?)Activator.CreateInstance(EventTypeUtils.ToType(type));
				if (e is IBaseEvent ei)
				{
					if (ei is IBarBeginningEvent eb)
					{
						level.Add(eb);
					}
					else
					{
						if (!count.TryGetValue(ei.Tab, out int value))
						{
							value = 0;
							count[ei.Tab] = value;
						}
						ei.Beat = new(count[ei.Tab] + 1);
						if (ei is BaseRowAction er)
						{
							if (er is BaseBeat beat)
							{
								if (er is AddClassicBeat or AddFreeTimeBeat or PulseFreeTimeBeat or SetRowXs)
									level.Rows[0].Add(beat);
								else
									level.Rows[1].Add(beat);
							}
							else
								level.Rows[0].Add(er);
						}
						else if (ei is BaseDecorationAction ed)
							level.Decorations[0].Add(ed);
						else
							level.Add(ei);
						count[ei.Tab] = ++value;
					}
				}
			}
			level.SaveToFile("out.rdlevel");
			level = RDLevel.FromJsonString(level.ToJsonString());
		}
		[TestMethod]
		public void ReadWriteSpeedTest()
		{
			Console.WriteLine("|Action\t|Elapsed\t|");
			var sw = System.Diagnostics.Stopwatch.StartNew();
			for (int i = 0; i < 10; i++)
			{
				sw.Start();
				using RDLevel level = RDLevel.FromFile("the-powe-S7V1kg9RWYK.rdzip");
				sw.Stop();
				Console.WriteLine($"|Read\t|{sw.ElapsedMilliseconds,10} ms\t|");
				sw.Reset();
				sw.Start();
				level.SaveToFile("out.rdlevel");
				sw.Stop();
				Console.WriteLine($"|Write\t|{sw.ElapsedMilliseconds,10} ms\t|");

				//foreach (var ad in level
				//	.OfEvents(EventType.FloatingText, EventType.AdvanceText)
				//	)
				//{
				//	Console.WriteLine(ad);
				//}
			}
		}
		[TestMethod]
		public void CPBTest()
		{
			RDLevel level = RDLevel.Default;
			SetCrotchetsPerBar cpb0 = new() { CrotchetsPerBar = 4 };
			SetCrotchetsPerBar cpb1 = new() { Beat = new(9), CrotchetsPerBar = 3 };
			SetCrotchetsPerBar cpb2 = new() { Beat = new(17), CrotchetsPerBar = 5 };
			Comment cmt1 = new() { Beat = new(2) };
			Comment cmt2 = new() { Beat = new(10) };
			Comment cmt3 = new() { Beat = new(19) };

			level.Add(cpb0);
			level.Add(cmt1);
			level.Add(cmt2);
			level.Add(cpb2);
			level.Add(cmt3);
			foreach(var cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for(int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();

			level.Add(cpb1);
			foreach(var cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for(int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();

			level.Remove(cpb1);
			foreach(var cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for (int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();

			level.Add(cpb1);
			foreach(var cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for (int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();
		}
	}
}
