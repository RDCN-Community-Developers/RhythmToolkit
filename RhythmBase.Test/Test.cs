using RhythmBase.Global.Extensions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Utils;
using System.Diagnostics;

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
			level.Rows.Add(new() { RowType = RowType.Classic });
			level.Rows.Add(new() { RowType = RowType.Oneshot });
			level.Decorations.Add([]);
			Dictionary<Tab, int> count = [];
			foreach (EventType type in ((EventType[])Enum.GetValues(typeof(EventType))).Where(i => !EventTypeUtils.CustomTypes.Contains(i)))
			{
				if (EventTypeUtils.CustomTypes.Contains(type))
					continue;
				if (type == EventType.AdvanceText)
					continue;
				IBaseEvent? e = (IBaseEvent?)Activator.CreateInstance(EventTypeUtils.ToType(type));
				if (e is null)
					continue;
				if (e.GetType().Name != e.Type.ToEnumString())
					throw new NotImplementedException();
				if (e is IBarBeginningEvent eb)
					level.Add(eb);
				else
				{
					if (!count.TryGetValue(e.Tab, out int value))
					{
						value = 0;
						count[e.Tab] = value;
					}
					e.Beat = new(count[e.Tab] + 1);
					if (e is BaseRowAction er)
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
					else if (e is BaseDecorationAction ed)
						level.Decorations[0].Add(ed);
					else
						level.Add(e);
					count[e.Tab] = ++value;
				}
			}
			level.SaveToFile("out.rdlevel");
			level = RDLevel.FromJsonString(level.ToJsonString());
		}
		[TestMethod]
		public void ReadWriteSpeedTest()
		{
			GlobalSettings.CachePath = "cache";
			{
				LevelReadOrWriteSettings settings = new()
				{
					InactiveEventsHandling = InactiveEventsHandling.Ignore,
					UnreadableEventsHandling = UnreadableEventHandling.Store,
					ZipFileProcessMethod = ZipFileProcessMethod.AllFiles,
					LoadAssets = false,
				};
				Console.WriteLine("|Action\t|Elapsed\t|");
				Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				for (int i = 0; i < 10; i++)
				{
					sw.Restart();
					using RDLevel level = RDLevel.FromFile("the-powe-S7V1kg9RWYK.rdzip", settings);
					Console.WriteLine($"|Read\t|{sw.ElapsedMilliseconds,10} ms\t|");
					sw.Restart();
					level.SaveToFile("out.rdlevel");
					Console.WriteLine($"|Write\t|{sw.ElapsedMilliseconds,10} ms\t|");
					foreach (var file in settings.FileReferences)
					{
						Console.WriteLine($"Cached file: {file.Path}");
					}
				}
			}
			//{
			//	LevelReadOrWriteSettings settings = new()
			//	{
			//		InactiveEventsHandling = InactiveEventsHandling.Ignore,
			//		UnreadableEventsHandling = UnreadableEventHandling.Store,
			//		ZipFileProcessMethod = ZipFileProcessMethod.LevelFileOnly,
			//	};
			//	Console.WriteLine("|Action\t|Elapsed\t|");
			//	Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
			//	for (int i = 0; i < 10; i++)
			//	{
			//		sw.Restart();
			//		using RDLevel level = RDLevel.FromFile("the-powe-S7V1kg9RWYK.rdzip", settings);
			//		Console.WriteLine($"|Read\t|{sw.ElapsedMilliseconds,10} ms\t|");
			//		sw.Restart();
			//		level.SaveToFile("out.rdlevel");
			//		Console.WriteLine($"|Write\t|{sw.ElapsedMilliseconds,10} ms\t|");
			//	}
			//}
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
			foreach (SetCrotchetsPerBar cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for (int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();

			level.Add(cpb1);
			foreach (SetCrotchetsPerBar cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for (int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();

			level.Remove(cpb1);
			foreach (SetCrotchetsPerBar cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for (int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();

			level.Add(cpb1);
			foreach (SetCrotchetsPerBar cpb in level.OfEvent<SetCrotchetsPerBar>())
			{
				Console.WriteLine(cpb);
			}
			for (int i = 1; i <= 20; i++)
			{
				Console.WriteLine($"{i}: {level.Calculator.BeatOf(i)}");
			}
			Console.WriteLine();
		}
		[TestMethod]
		public void EventTypeCollectionsTest()
		{
			var allTypes = (EventType[])Enum.GetValues(typeof(EventType));
			foreach (var type in EventTypeUtils.CustomTypes)
				Console.WriteLine(type);
		}
	}
}
