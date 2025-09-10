using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>
	/// Beat calculator.
	/// </summary>
	public class BeatCalculator
	{
		internal BeatCalculator(RDLevel level)
		{
			Collection = level;
			Refresh(true);
		}
		/// <summary>
		/// Refresh the cache.
		/// </summary>
		public void Refresh(bool init = false)
		{
			if (init)
			{
				_BPMList = [];
				_CPBList = [];
			}
			else
			{
				_BPMList = Collection.OfEvent<BaseBeatsPerMinute>()
							.ToList();
				_CPBList = Collection.OfEvent<SetCrotchetsPerBar>()
							.ToList();
			}
		}
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		/// <returns>Total 1-based beats.</returns>
		public float BarBeatToBeatOnly(int bar, float beat) => BarBeatToBeatOnly(bar, beat, _CPBList);
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
		public (int bar, float beat) BeatOnlyToBarBeat(float beat) => BeatOnlyToBarBeat(beat, _CPBList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="beat">Total 1-based beats.</param>
		/// <returns>Total time span.</returns>
		public TimeSpan BeatOnlyToTimeSpan(float beat) => BeatOnlyToTimeSpan(beat, _BPMList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="timeSpan">Total time span.</param>
		/// <returns>Total 1-based beats.</returns>
		public float TimeSpanToBeatOnly(TimeSpan timeSpan) => TimeSpanToBeatOnly(timeSpan, _BPMList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="timeSpan">Total time span.</param>
		/// <returns>The 1-based bar and the 1-based beat of bar.</returns>
		public (int bar, float beat) TimeSpanToBarBeat(TimeSpan timeSpan) => BeatOnlyToBarBeat(TimeSpanToBeatOnly(timeSpan));
		private static float BarBeatToBeatOnly(int bar, float beat, IEnumerable<SetCrotchetsPerBar> Collection)
		{
			(float BeatOnly, int Bar, int CPB) foreCPB = new(1f, 1, 8);
			SetCrotchetsPerBar? LastCPB = Collection.LastOrDefault((i) => i.Active && i.Beat.BarBeat.bar < bar);
			if (LastCPB != null)
				foreCPB = new(LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar);
			return foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1f;
		}
		private static (int bar, float beat) BeatOnlyToBarBeat(float beat, IEnumerable<SetCrotchetsPerBar> Collection)
		{
			(float BeatOnly, int Bar, int CPB) foreCPB = new(1f, 1, 8);
			SetCrotchetsPerBar? LastCPB = Collection.LastOrDefault((i) => i.Active && i.Beat.BeatOnly < beat);
			if (LastCPB != null)
				foreCPB = new(LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar);
			(int bar, float beat) result = ((int)Math.Round(foreCPB.Bar + Math.Floor((double)((beat - foreCPB.BeatOnly) / foreCPB.CPB))), (beat - foreCPB.BeatOnly) % foreCPB.CPB + 1f);
			return result;
		}
		private static TimeSpan BeatOnlyToTimeSpan(float beatOnly, IEnumerable<BaseBeatsPerMinute> BPMCollection)
		{
			ValueTuple<float, float> fore = new(1f, Utils.DefaultBPM);
			BaseBeatsPerMinute? foreBPM = BPMCollection.FirstOrDefault();
			if (foreBPM != null)
				fore = new ValueTuple<float, float>(foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute);
			float resultMinute = 0f;
			foreach (BaseBeatsPerMinute item in BPMCollection)
				if (beatOnly > item.Beat.BeatOnly)
				{
					resultMinute += (item.Beat.BeatOnly - fore.Item1) / fore.Item2;
					fore = new ValueTuple<float, float>(item.Beat.BeatOnly, item.BeatsPerMinute);
				}
			resultMinute += (beatOnly - fore.Item1) / fore.Item2;
			return TimeSpan.FromMinutes((double)resultMinute);
		}
		private static float TimeSpanToBeatOnly(TimeSpan timeSpan, IEnumerable<BaseBeatsPerMinute> BPMCollection)
		{
			ValueTuple<float, float> fore = new(1f, Utils.DefaultBPM);
			BaseBeatsPerMinute? foreBPM = BPMCollection.FirstOrDefault();
			if (foreBPM != null)
				fore = new ValueTuple<float, float>(foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute);
			float beatOnly = 1f;
			foreach (BaseBeatsPerMinute item in BPMCollection)
				if (timeSpan > BeatOnlyToTimeSpan(item.Beat.BeatOnly, BPMCollection))
					beatOnly = (float)((double)beatOnly + (BeatOnlyToTimeSpan(item.Beat.BeatOnly, BPMCollection) - BeatOnlyToTimeSpan(fore.Item1, BPMCollection)).TotalMinutes * fore.Item2);
			beatOnly = (float)((double)beatOnly + (timeSpan - BeatOnlyToTimeSpan(fore.Item1, BPMCollection)).TotalMinutes * fore.Item2);
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
		public float BeatsPerMinuteOf(RDBeat beat) => _BPMList.LastOrDefault((i) => i.Beat < beat)?.BeatsPerMinute ?? Utils.DefaultBPM;
		/// <summary>
		/// Calculate the CPB of the moment in which the beat is.
		/// </summary>
		public float CrotchetsPerBarOf(RDBeat beat) => _CPBList.LastOrDefault((i) => i.Beat < beat)?.CrotchetsPerBar ?? 8;
		internal readonly RDLevel Collection;
		private List<BaseBeatsPerMinute> _BPMList = [];
		private List<SetCrotchetsPerBar> _CPBList = [];
	}
}
