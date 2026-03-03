using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;

namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>
	/// Describes how beat data should be preserved or shifted when crotchets-per-bar (CPB) changes occur.
	/// </summary>
	[Flags]
	public enum BeatChangeStrategy : byte
	{
		/// <summary>
		/// Keeps the global beat positions fixed while adjusting bar boundaries.
		/// </summary>
		KeepGlobalBeat = 0b00, // 保持全局节拍不动

		/// <summary>
		/// Keeps beats relative to their current bar, allowing global positions to shift.
		/// </summary>
		KeepBarRelativeBeat = 0b01, // 保持小节内相对于自身所在小节的节拍不动

		/// <summary>
		/// Keeps beats in the affected region aligned relative to the first bar within that region.
		/// </summary>
		KeepChangeRelativeToFirstBar = 0b00, // 保持变动部分小节内相对于第一个小节的节拍不动

		/// <summary>
		/// Keeps beats in the affected region aligned relative to each bar inside the region itself.
		/// </summary>
		KeepChangeRelativeBeat = 0b10, // 保持变动部分小节内相对于自身所在小节的节拍不动	

		/// <summary>
		/// Default engine behavior, equivalent to <see cref="KeepGlobalBeat"/>.
		/// </summary>
		Default = 0b00,

		/// <summary>
		/// Rhythm Doctor Level Editor behavior, combining bar relative placement in both scopes.
		/// </summary>
		RDLE = 0b11,
	}

	/// <summary>
	/// Represents a cached BPM anchor used to quickly convert between beats and time spans.
	/// </summary>
	/// <param name="BeatOnly">Absolute beat where the BPM takes effect.</param>
	/// <param name="TimeSpan">Absolute time when the BPM takes effect.</param>
	/// <param name="Bpm">Beats per minute value.</param>
	internal record struct BpmCache(float BeatOnly, TimeSpan TimeSpan, float Bpm) : IComparable<BpmCache>
	{
		/// <summary>
		/// Gets the fallback BPM cache representing the initial state.
		/// </summary>
		public static readonly BpmCache Default = new(1, TimeSpan.Zero, Utils.DefaultBPM);

		/// <inheritdoc />
		public readonly int CompareTo(BpmCache other) => BeatOnly.CompareTo(other.BeatOnly);
	}

	/// <summary>
	/// Represents a cached CPB anchor used to translate between global beats and bar-relative beats.
	/// </summary>
	/// <param name="BeatOnly">Absolute beat where the CPB change occurs.</param>
	/// <param name="Bar">One-based bar index for the CPB anchor.</param>
	/// <param name="Cpb">Number of crotchets contained in one bar.</param>
	internal record struct CpbCache(float BeatOnly, int Bar, int Cpb) : IComparable<CpbCache>
	{
		/// <summary>
		/// Gets the fallback CPB cache representing the initial state.
		/// </summary>
		public static readonly CpbCache Default = new(1, 1, Utils.DefaultCPB);

		/// <inheritdoc />
		public readonly int CompareTo(CpbCache other) => BeatOnly.CompareTo(other.BeatOnly);
	};

	/// <summary>
	/// Beat calculator that converts between absolute beats, bars, and time spans, while reacting to tempo and CPB changes.
	/// </summary>
	public class BeatCalculator
	{
		/// <summary>
		/// Provides access to the underlying level collection that stores beat-ordered events.
		/// </summary>
		internal readonly RDLevel Collection;

		/// <summary>
		/// Cached BPM anchors sorted by beat order for fast lookup.
		/// </summary>
		private BpmCache[] _bpmCache = [];

		/// <summary>
		/// Cached CPB anchors sorted by beat order for fast lookup.
		/// </summary>
		private CpbCache[] _cpbCache = [];

		/// <summary>
		/// Initializes a new instance of the <see cref="BeatCalculator"/> class.
		/// </summary>
		/// <param name="level">The owning level collection that provides beat events.</param>
		internal BeatCalculator(RDLevel level)
		{
			Collection = level;
		}

		/// <summary>
		/// Inserts a CPB change at the specified location and optionally returns a corrective entry when bar alignment requires it.
		/// </summary>
		/// <param name="cpb">The CPB cache to insert.</param>
		/// <param name="strategy">The beat-change strategy to apply.</param>
		/// <param name="fix">Outputs a corrective CPB entry when additional adjustments are required.</param>
		/// <returns><c>true</c> if an extra corrective CPB entry was produced; otherwise <c>false</c>.</returns>
		internal bool AddCpbAt(CpbCache cpb, byte strategy, out CpbCache fix)
		{
			fix = CpbCache.Default;
			if (_cpbCache.Length == 0)
			{
				_cpbCache = [cpb];
				return false;
			}
			int index = _cpbCache.BinarySearch(cpb);
			if (index < 0) index = ~index - 1;

			CpbCache a, b;
			if (index < 0) a = CpbCache.Default;
			else a = _cpbCache[index];
			if (index >= _cpbCache.Length - 1)
			{
				var col = Collection.eventsBeatOrder;
				int dbeatPerBar = cpb.Cpb - a.Cpb;
				float st = cpb.BeatOnly + cpb.Cpb;
				KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>>[] nodes = [.. Collection.eventsBeatOrder.Where(e => e.Key.BeatOnly >= st)];
				foreach (var node in nodes)
				{
					(int bar2, _) = node.Key;
					int offset = (bar2 - cpb.Bar) * dbeatPerBar;
					col.Remove(node);
					foreach (IBaseEvent e in node.Value)
					{
						BaseEvent _e = (e as BaseEvent)!;
						_e._beat += offset;
					}
					col.Insert(new(this, node.Key.BeatOnly + offset), node.Value);
				}
				_cpbCache = [.. _cpbCache, cpb];
				return false;
			}
			else
			{
				b = _cpbCache[index + 1];
				if (a.Cpb == cpb.Cpb)
				{
					return false;
				}
				bool needInsert = true;
				if (cpb.BeatOnly == a.BeatOnly)
				{
					_cpbCache[index] = cpb;
					needInsert = false;
				}
				if ((strategy & 0b01) == 0)
				{
					int diff = (int)(b.BeatOnly - cpb.BeatOnly) % cpb.Cpb;
					int barDiff = (int)((b.BeatOnly - cpb.BeatOnly) / cpb.Cpb);
					if (diff == 0)
					{
						if (needInsert)
							_cpbCache = [.. _cpbCache.Take(index + 1), cpb, .. _cpbCache.Skip(index + 1).Select(c => c with { Bar = c.Bar - (b.Bar - cpb.Bar) + barDiff })];
						else
							_cpbCache = [.. _cpbCache.Take(index + 1), .. _cpbCache.Skip(index + 1).Select(c => c with { Bar = c.Bar - (b.Bar - cpb.Bar) + barDiff })];
						return false;
					}
					else
					{
						fix = new(
							cpb.BeatOnly + barDiff * cpb.Cpb,
							cpb.Bar + barDiff,
							diff
							);
						barDiff += 1;
						if (needInsert)
							_cpbCache = [.. _cpbCache.Take(index + 1), cpb, fix, .. _cpbCache.Skip(index + 1).Select(c => c with { Bar = c.Bar - (b.Bar - cpb.Bar) + barDiff })];
						else
							_cpbCache = [.. _cpbCache.Take(index + 1), fix, .. _cpbCache.Skip(index + 1).Select(c => c with { Bar = c.Bar - (b.Bar - cpb.Bar) + barDiff })];
						return true;
					}
				}
				else
				{
					int dbeatPerBar = cpb.Cpb - a.Cpb;
					int barCount = b.Bar - cpb.Bar;
					int dbeat = dbeatPerBar * barCount;
					var col = Collection.eventsBeatOrder;
					float st = (strategy & 0b010) == 0 ? cpb.BeatOnly + cpb.Cpb : b.BeatOnly;
					KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>>[] nodes = [.. Collection.eventsBeatOrder.Where(e => e.Key.BeatOnly >= st)];
					foreach (var node in nodes)
					{
						(int bar2, _) = node.Key;
						int offset = (strategy & 0b10) == 0 ? (bar2 < b.Bar ? (bar2 - cpb.Bar) * dbeatPerBar : dbeat) : dbeat;
						col.Remove(node);
						foreach (IBaseEvent e in node.Value)
						{
							BaseEvent _e = (e as BaseEvent)!;
							_e._beat += offset;
						}
						col.Insert(new(this, node.Key.BeatOnly + offset), node.Value);
					}
					if (needInsert)
						_cpbCache = [.. _cpbCache.Take(index + 1), cpb, .. _cpbCache.Skip(index + 1).Select(c => c with { BeatOnly = c.BeatOnly + dbeat })];
					else
						_cpbCache = [.. _cpbCache.Take(index + 1), .. _cpbCache.Skip(index + 1).Select(c => c with { BeatOnly = c.BeatOnly + dbeat })];
					return false;
				}
			}
		}

		/// <summary>
		/// Removes a CPB change and optionally returns a corrective entry when bar alignment requires it.
		/// </summary>
		/// <param name="cpb">The CPB cache entry to remove.</param>
		/// <param name="strategy">The beat-change strategy to apply.</param>
		/// <param name="fix">Outputs a corrective CPB entry when additional adjustments are required.</param>
		/// <returns><c>true</c> if an extra corrective CPB entry was produced; otherwise <c>false</c>.</returns>
		internal bool RemoveCpbAt(CpbCache cpb, byte strategy, out CpbCache fix)
		{
			fix = CpbCache.Default;
			if (_cpbCache.Length == 0)
				return false;
			int index = _cpbCache.BinarySearch(cpb);
			if (index < 0) return false;

			CpbCache a, b;
			if (index == 0) a = CpbCache.Default;
			else a = _cpbCache[index - 1];
			if (index == _cpbCache.Length - 1)
			{
				var col = Collection.eventsBeatOrder;
				int dbeatPerBar = a.Cpb - cpb.Cpb;
				float st = cpb.BeatOnly + cpb.Cpb;
				KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>>[] nodes = [.. Collection.eventsBeatOrder.Where(e => e.Key.BeatOnly >= st)];
				foreach (var node in nodes)
				{
					(int bar2, _) = node.Key;
					int offset = (bar2 - cpb.Bar) * dbeatPerBar;
					col.Remove(node);
					foreach (IBaseEvent e in node.Value)
					{
						BaseEvent _e = (e as BaseEvent)!;
						_e._beat += offset;
					}
					col.Insert(new(this, node.Key.BeatOnly + offset), node.Value);
				}
				_cpbCache = [.. _cpbCache.Take(_cpbCache.Length - 1)];
				return false;
			}
			else
			{
				b = _cpbCache[index + 1];
				int lenac = (int)(cpb.BeatOnly - a.BeatOnly);
				int lencb = (int)(b.BeatOnly - cpb.BeatOnly);
				if (a.Cpb == cpb.Cpb)
				{
					_cpbCache = [.. _cpbCache.Take(index), .. _cpbCache.Skip(index + 1)];
					return false;
				}
				if ((strategy & 0b01) == 0)
				{
					int diff = (int)(b.BeatOnly - cpb.BeatOnly) % a.Cpb;
					int barDiff = (int)((b.BeatOnly - cpb.BeatOnly) / a.Cpb);
					if (diff == 0)
					{
						_cpbCache = [.. _cpbCache.Take(index), .. _cpbCache.Skip(index + 1).Select(c => c with { Bar = c.Bar - (b.Bar - a.Bar) + barDiff })];
						return false;
					}
					else
					{
						fix = new(
							cpb.BeatOnly + barDiff * a.Cpb,
							cpb.Bar + barDiff,
							diff
							);
						barDiff += 1;
						_cpbCache = [.. _cpbCache.Take(index), fix, .. _cpbCache.Skip(index + 1).Select(c => c with { Bar = c.Bar - (b.Bar - a.Bar) + barDiff })];
						return true;
					}
				}
				else
				{
					int dbeatPerBar = a.Cpb - cpb.Cpb;
					int barCount = b.Bar - cpb.Bar;
					int dbeat = dbeatPerBar * barCount;
					var col = Collection.eventsBeatOrder;
					float st = (strategy & 0b10) == 0 ? cpb.BeatOnly + cpb.Cpb : b.BeatOnly;
					KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>>[] nodes = [.. Collection.eventsBeatOrder.Where(e => e.Key.BeatOnly >= st)];
					foreach (var node in nodes)
					{
						(int bar2, _) = node.Key;
						int offset = (strategy & 0b10) == 0 ? (bar2 < b.Bar ? (bar2 - cpb.Bar) * dbeatPerBar : dbeat) : dbeat;
						col.Remove(node);
						foreach (IBaseEvent e in node.Value)
						{
							BaseEvent _e = (e as BaseEvent)!;
							_e._beat += offset;
						}
						col.Insert(new(this, node.Key.BeatOnly + offset), node.Value);
					}
					_cpbCache = [.. _cpbCache.Take(index), .. _cpbCache.Skip(index + 1).Select(c => c with { BeatOnly = c.BeatOnly + dbeat })];
					return false;
				}
			}
		}

		/// <summary>
		/// Refreshes the BPM and CPB caches so subsequent conversions use up-to-date values.
		/// </summary>
		public void Refresh()
		{
			SetCrotchetsPerBar[] cpbList = [.. Collection.OfEvent<SetCrotchetsPerBar>()];
			BaseBeatsPerMinute[] bpmList = [.. Collection.OfEvent<BaseBeatsPerMinute>()];
			_cpbCache = new CpbCache[cpbList.Length];
			_bpmCache = new BpmCache[bpmList.Length];
			for (int i = 0; i < cpbList.Length; i++)
			{
				(int bar, _) = cpbList[i].Beat;
				_cpbCache[i] = new CpbCache(cpbList[i].Beat.BeatOnly, bar, cpbList[i].CrotchetsPerBar);
			}
			for (int i = 0; i < bpmList.Length; i++)
			{
				_bpmCache[i] = new BpmCache(bpmList[i].Beat.BeatOnly, bpmList[i].Beat.TimeSpan, bpmList[i].BeatsPerMinute);
			}
		}

		/// <summary>
		/// Converts a bar/beat pair to its absolute beat position using the current cache state.
		/// </summary>
		/// <param name="bar">The one-based bar index.</param>
		/// <param name="beat">The one-based beat within the bar.</param>
		/// <returns>The absolute beat position.</returns>
		public float BarBeatToBeatOnly(int bar, float beat) => BarBeatToBeatOnly(bar, beat, in _cpbCache);

		/// <summary>
		/// Converts a bar/beat pair to an absolute timespan using the current cache state.
		/// </summary>
		/// <param name="bar">The one-based bar index.</param>
		/// <param name="beat">The one-based beat within the bar.</param>
		/// <returns>The absolute time represented by the beat.</returns>
		public TimeSpan BarBeatToTimeSpan(int bar, float beat) => BeatOnlyToTimeSpan(BarBeatToBeatOnly(bar, beat));

		/// <summary>
		/// Converts an absolute beat position to its bar/beat representation using the current cache state.
		/// </summary>
		/// <param name="beat">The absolute beat position.</param>
		/// <returns>The one-based bar index and the one-based beat within that bar.</returns>
		public (int bar, float beat) BeatOnlyToBarBeat(float beat) => BeatOnlyToBarBeat(beat, in _cpbCache);

		/// <summary>
		/// Converts an absolute beat position to an absolute timespan using the current cache state.
		/// </summary>
		/// <param name="beat">The absolute beat position.</param>
		/// <returns>The corresponding absolute time.</returns>
		public TimeSpan BeatOnlyToTimeSpan(float beat) => BeatOnlyToTimeSpan(beat, in _bpmCache);

		/// <summary>
		/// Converts an absolute timespan to an absolute beat position using the current cache state.
		/// </summary>
		/// <param name="timeSpan">The absolute time to convert.</param>
		/// <returns>The corresponding absolute beat.</returns>
		public float TimeSpanToBeatOnly(TimeSpan timeSpan) => TimeSpanToBeatOnly(timeSpan, in _bpmCache);

		/// <summary>
		/// Converts an absolute timespan to its bar/beat representation using the current cache state.
		/// </summary>
		/// <param name="timeSpan">The absolute time to convert.</param>
		/// <returns>The one-based bar index and the one-based beat within that bar.</returns>
		public (int bar, float beat) TimeSpanToBarBeat(TimeSpan timeSpan) => BeatOnlyToBarBeat(TimeSpanToBeatOnly(timeSpan));

		/// <summary>
		/// Converts a bar/beat pair to an absolute beat using the specified CPB cache set.
		/// </summary>
		/// <param name="bar">The one-based bar index.</param>
		/// <param name="beat">The one-based beat within the bar.</param>
		/// <param name="cacheSet">The CPB cache to consult.</param>
		/// <returns>The absolute beat position.</returns>
		private static float BarBeatToBeatOnly(int bar, float beat, in CpbCache[] cacheSet)
		{
			CpbCache last = CpbCache.Default;
			foreach (var cache in cacheSet)
			{
				int cbar = cache.Bar;
				if (cbar < bar)
				{
					last = cache;
					continue;
				}
				if (cbar == bar)
				{
					float beatOnly = cache.BeatOnly + beat - 1f;
					return beatOnly;
				}
				break;
			}
			float finalBeatOnly = last.BeatOnly + (bar - last.Bar) * last.Cpb + beat - 1;
			return finalBeatOnly;
		}

		/// <summary>
		/// Converts an absolute beat to a bar/beat pair using the specified CPB cache set.
		/// </summary>
		/// <param name="beat">The absolute beat position.</param>
		/// <param name="cacheSet">The CPB cache to consult.</param>
		/// <returns>The one-based bar index and the one-based beat within that bar.</returns>
		private static (int bar, float beat) BeatOnlyToBarBeat(float beat, in CpbCache[] cacheSet)
		{
			CpbCache last = CpbCache.Default;
			foreach (CpbCache cache in cacheSet)
			{
				float cbeat = cache.BeatOnly;
				if (cbeat < beat)
				{
					last = cache;
					continue;
				}
				if (cbeat == beat)
					return (cache.Bar, 1f);
				break;
			}
			(int finalBar, float finalBeat) result2 = ((int)Math.Round(last.Bar + Math.Floor((double)((beat - last.BeatOnly) / last.Cpb))), (beat - last.BeatOnly) % last.Cpb + 1f);
			return result2;
		}

		/// <summary>
		/// Converts an absolute beat to an absolute timespan using the specified BPM cache set.
		/// </summary>
		/// <param name="beatOnly">The absolute beat position.</param>
		/// <param name="cacheSet">The BPM cache to consult.</param>
		/// <returns>The corresponding absolute time.</returns>
		private static TimeSpan BeatOnlyToTimeSpan(float beatOnly, in BpmCache[] cacheSet)
		{
			BpmCache last = BpmCache.Default;
			foreach (BpmCache cache in cacheSet)
			{
				float cbeat = cache.BeatOnly;
				if (cbeat < beatOnly)
				{
					last = cache;
					continue;
				}
				if (cbeat == beatOnly)
					return cache.TimeSpan;
				break;
			}
			float durationFromLast = (beatOnly - last.BeatOnly) / last.Bpm;
			TimeSpan result = last.TimeSpan + TimeSpan.FromMinutes(durationFromLast);
			return result;
		}

		/// <summary>
		/// Converts an absolute timespan to an absolute beat using the specified BPM cache set.
		/// </summary>
		/// <param name="timeSpan">The absolute time value.</param>
		/// <param name="cacheSet">The BPM cache to consult.</param>
		/// <returns>The corresponding absolute beat.</returns>
		private static float TimeSpanToBeatOnly(TimeSpan timeSpan, in BpmCache[] cacheSet)
		{
			BpmCache last = BpmCache.Default;
			foreach (BpmCache cache in cacheSet)
			{
				TimeSpan ctime = cache.TimeSpan;
				if (ctime < timeSpan)
				{
					last = cache;
					continue;
				}
				if (ctime == timeSpan)
					return cache.BeatOnly;
				break;
			}
			float beatOnly = last.BeatOnly + (float)((timeSpan - last.TimeSpan).TotalMinutes * last.Bpm);
			return beatOnly;
		}

		/// <summary>
		/// Creates an <see cref="RDBeat"/> from an absolute beat value.
		/// </summary>
		/// <param name="beatOnly">The absolute beat position.</param>
		/// <returns>An <see cref="RDBeat"/> bound to this calculator.</returns>
		public RDBeat BeatOf(float beatOnly) => new(this, beatOnly);

		/// <summary>
		/// Creates an <see cref="RDBeat"/> from a bar/beat pair.
		/// </summary>
		/// <param name="bar">The one-based bar index.</param>
		/// <param name="beat">The one-based beat within the bar.</param>
		/// <returns>An <see cref="RDBeat"/> bound to this calculator.</returns>
		public RDBeat BeatOf(int bar, float beat) => new(this, bar, beat);

		/// <summary>
		/// Creates an <see cref="RDBeat"/> from an absolute timespan.
		/// </summary>
		/// <param name="timeSpan">The absolute time to convert.</param>
		/// <returns>An <see cref="RDBeat"/> bound to this calculator.</returns>
		public RDBeat BeatOf(TimeSpan timeSpan) => new(this, timeSpan);

		/// <summary>
		/// Creates an <see cref="RDRange"/> representing the interval between two beats.
		/// </summary>
		/// <param name="beat1">The starting beat.</param>
		/// <param name="beat2">The ending beat.</param>
		/// <returns>The resulting interval.</returns>
		public RDRange IntervalOf(RDBeat beat1, RDBeat beat2) => new(new(this, beat1), new(this, beat2));

		/// <summary>
		/// Creates an <see cref="RDRange"/> representing the interval between two bar/beat pairs.
		/// </summary>
		/// <param name="beat1">The starting bar/beat tuple.</param>
		/// <param name="beat2">The ending bar/beat tuple.</param>
		/// <returns>The resulting interval.</returns>
		public RDRange IntervalOf((int bar, float beat) beat1, (int bar, float beat) beat2) => IntervalOf(BeatOf(beat1.bar, beat1.beat), BeatOf(beat2.bar, beat2.beat));

		/// <summary>
		/// Creates an <see cref="RDRange"/> representing the interval between two timespans.
		/// </summary>
		/// <param name="timeSpan1">The start time.</param>
		/// <param name="timeSpan2">The end time.</param>
		/// <returns>The resulting interval.</returns>
		public RDRange IntervalOf(TimeSpan timeSpan1, TimeSpan timeSpan2) => IntervalOf(BeatOf(timeSpan1), BeatOf(timeSpan2));

		/// <summary>
		/// Gets the BPM in effect at the specified beat.
		/// </summary>
		/// <param name="beat">The beat whose BPM should be retrieved.</param>
		/// <returns>The BPM active at the beat.</returns>
		public float BeatsPerMinuteOf(RDBeat beat)
		{
			BpmCache last = BpmCache.Default;
			foreach (BpmCache cache in _bpmCache)
			{
				float cbeat = cache.BeatOnly;
				if (cbeat < beat.BeatOnly)
				{
					last = cache;
					continue;
				}
				if (cbeat == beat.BeatOnly)
					return cache.Bpm;
				break;
			}
			return last.Bpm;
		}

		/// <summary>
		/// Gets the CPB in effect at the specified beat.
		/// </summary>
		/// <param name="beat">The beat whose CPB should be retrieved.</param>
		/// <returns>The CPB active at the beat.</returns>
		public int CrotchetsPerBarOf(RDBeat beat)
		{
			CpbCache last = CpbCache.Default;
			foreach (CpbCache cache in _cpbCache)
			{
				float cbeat = cache.BeatOnly;
				if (cbeat < beat.BeatOnly)
				{
					last = cache;
					continue;
				}
				if (cbeat == beat.BeatOnly)
					return cache.Cpb;
				break;
			}
			return last.Cpb;
		}
	}
}
