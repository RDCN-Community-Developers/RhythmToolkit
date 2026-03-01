using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.RhythmDoctor.Utils
{
	internal record struct BpmCache(float BeatOnly, TimeSpan TimeSpan, float Bpm) : IComparable<BpmCache>
	{
		public static readonly BpmCache Default = new(1, TimeSpan.Zero, Utils.DefaultBPM);
		public int CompareTo(BpmCache other) => BeatOnly.CompareTo(other.BeatOnly);
	}
	internal record struct CpbCache(float BeatOnly, int Bar, int Cpb) : IComparable<CpbCache>
	{
		public static readonly CpbCache Default = new(1, 1, Utils.DefaultCPB);
		public int CompareTo(CpbCache other) => BeatOnly.CompareTo(other.BeatOnly);
	};
	/// <summary>
	/// Beat calculator.
	/// </summary>
	public class BeatCalculator
	{
		internal readonly RDLevel Collection;
		//private RedBlackTree<RDBeat, List<BaseBeatsPerMinute>> _BpmTree = [];
		//private RedBlackTree<RDBeat, List<SetCrotchetsPerBar>> _CpbTree = [];
		private readonly SortedSet<BpmCache> _bpmCache = [];
		private readonly SortedSet<CpbCache> _cpbCache = [];
		internal BeatCalculator(RDLevel level)
		{
			Collection = level;
		}
		internal void Update(SetCrotchetsPerBar cpb)
		{
			(int bar, _) = cpb.Beat;
			_cpbCache.Add(new CpbCache(cpb.Beat.BeatOnly, bar, cpb.CrotchetsPerBar));
		}
		internal void Update(BaseBeatsPerMinute bpm)
		{
			TimeSpan timeSpan = bpm.Beat.TimeSpan;
			_bpmCache.Add(new BpmCache(bpm.Beat.BeatOnly, timeSpan, bpm.BeatsPerMinute));
		}
		/// <summary>
		/// Refresh the cache.
		/// </summary>
		public void Refresh()
		{
			_cpbCache.Clear();
			_bpmCache.Clear();
			foreach (SetCrotchetsPerBar cpb in Collection.OfEvent<SetCrotchetsPerBar>())
				Update(cpb);
			foreach (BaseBeatsPerMinute bpm in Collection.OfEvent<BaseBeatsPerMinute>())
				Update(bpm);
		}
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		/// <returns>Total 1-based beats.</returns>
		public float BarBeatToBeatOnly(int bar, float beat) => BarBeatToBeatOnly(bar, beat, in _cpbCache);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		/// <returns>Total time span.</returns>
		public TimeSpan BarBeatToTimeSpan(int bar, float beat) => BeatOnlyToTimeSpan(BarBeatToBeatOnly(bar, beat));
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="beat">Total 1-based beats.</param>
		/// <returns>The 1-based bar and the 1-based beat of bar.</returns>
		public (int bar, float beat) BeatOnlyToBarBeat(float beat) => BeatOnlyToBarBeat(beat, in _cpbCache);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="beat">Total 1-based beats.</param>
		/// <returns>Total time span.</returns>
		public TimeSpan BeatOnlyToTimeSpan(float beat) => BeatOnlyToTimeSpan(beat, in _bpmCache);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="timeSpan">Total time span.</param>
		/// <returns>Total 1-based beats.</returns>
		public float TimeSpanToBeatOnly(TimeSpan timeSpan) => TimeSpanToBeatOnly(timeSpan, in _bpmCache);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="timeSpan">Total time span.</param>
		/// <returns>The 1-based bar and the 1-based beat of bar.</returns>
		public (int bar, float beat) TimeSpanToBarBeat(TimeSpan timeSpan) => BeatOnlyToBarBeat(TimeSpanToBeatOnly(timeSpan));
		private static float BarBeatToBeatOnly(int bar, float beat, in SortedSet<CpbCache> cacheSet)
		{
			CpbCache last = CpbCache.Default;
			foreach (var cache in cacheSet)
			{
				int cbar = cache.Bar;
				if(cbar < bar)
				{
					last = cache;
					continue;
				}
				if(cbar ==  bar)
				{
					float beatOnly = cache.BeatOnly + beat - 1f;
					return beatOnly;
				}
				break;
			}
			float finalBeatOnly = last.BeatOnly + (bar - last.Bar) * last.Cpb + beat - 1;
			return finalBeatOnly;
		}
		private static (int bar, float beat) BeatOnlyToBarBeat(float beat, in SortedSet<CpbCache> cacheSet)
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
		private static TimeSpan BeatOnlyToTimeSpan(float beatOnly, in SortedSet<BpmCache> cacheSet)
		{
			BpmCache last = BpmCache.Default;
			foreach(BpmCache cache in cacheSet)
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
		private static float TimeSpanToBeatOnly(TimeSpan timeSpan, in SortedSet<BpmCache> cacheSet)
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
		/// Creates a beat instance.
		/// </summary>
		public RDBeat BeatOf(float beatOnly) => new(this, beatOnly);
		/// <summary>
		/// Creates a beat instance.
		/// </summary>
		public RDBeat BeatOf(int bar, float beat) => new(this, bar, beat);
		/// <summary>
		/// Creates a beat instance.
		/// </summary>
		public RDBeat BeatOf(TimeSpan timeSpan) => new(this, timeSpan);
		/// <summary>
		/// Creates an interval between two beats.
		/// </summary>
		/// <param name="beat1">The first beat.</param>
		/// <param name="beat2">The second beat.</param>
		/// <returns>An RDRange representing the interval between the two beats.</returns>
		public RDRange IntervalOf(RDBeat beat1, RDBeat beat2) => new(new(this, beat1), new(this, beat2));
		/// <summary>
		/// Creates an interval between two beats specified by bar and beat.
		/// </summary>
		/// <param name="beat1">The first beat specified by bar and beat.</param>
		/// <param name="beat2">The second beat specified by bar and beat.</param>
		/// <returns>An RDRange representing the interval between the two beats.</returns>
		public RDRange IntervalOf((int bar, float beat) beat1, (int bar, float beat) beat2) => IntervalOf(BeatOf(beat1.bar, beat1.beat), BeatOf(beat2.bar, beat2.beat));
		/// <summary>
		/// Creates an interval between two beats specified by time spans.
		/// </summary>
		/// <param name="timeSpan1">The first time span.</param>
		/// <param name="timeSpan2">The second time span.</param>
		/// <returns>An RDRange representing the interval between the two time spans.</returns>
		public RDRange IntervalOf(TimeSpan timeSpan1, TimeSpan timeSpan2) => IntervalOf(BeatOf(timeSpan1), BeatOf(timeSpan2));
		/// <summary>
		/// Calculate the BPM of the moment in which the beat is.
		/// </summary>
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
		/// Calculate the CPB of the moment in which the beat is.
		/// </summary>
		public float CrotchetsPerBarOf(RDBeat beat)
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
