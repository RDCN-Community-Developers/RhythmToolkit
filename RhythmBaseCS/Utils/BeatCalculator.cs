using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Extensions;
namespace RhythmBase.Utils
{
	/// <summary>
	/// Beat calculator.
	/// </summary>
	public class BeatCalculator
	{
		internal BeatCalculator(RDLevel level)
		{
			Collection = level;
			Refresh();
		}
		/// <summary>
		/// Refresh the cache.
		/// </summary>
		public void Refresh()
		{
			_BPMList = Collection.Where<BaseBeatsPerMinute>()
						.ToList();
			_CPBList = Collection.Where<SetCrotchetsPerBar>()
						.ToList();
		}
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		/// <returns>Total 1-based beats.</returns>
		internal float BarBeatToBeatOnly(uint bar, float beat) => BarBeatToBeatOnly(bar, beat, _CPBList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="bar">The 1-based bar.</param>
		/// <param name="beat">The 1-based beat of the bar.</param>
		/// <returns>Total time span.</returns>
		public TimeSpan BarBeatToTimeSpan(uint bar, float beat) => BeatOnlyToTimeSpan(BarBeatToBeatOnly(bar, beat));
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="beat">Total 1-based beats.</param>
		/// <returns>The 1-based bar and the 1-based beat of bar.</returns>
		internal (uint bar, float beat) BeatOnlyToBarBeat(float beat) => BeatOnlyToBarBeat(beat, _CPBList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="beat">Total 1-based beats.</param>
		/// <returns>Total time span.</returns>
		internal TimeSpan BeatOnlyToTimeSpan(float beat) => BeatOnlyToTimeSpan(beat, _BPMList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="timeSpan">Total time span.</param>
		/// <returns>Total 1-based beats.</returns>
		internal float TimeSpanToBeatOnly(TimeSpan timeSpan) => TimeSpanToBeatOnly(timeSpan, _BPMList);
		/// <summary>
		/// Convert beat data.
		/// </summary>
		/// <param name="timeSpan">Total time span.</param>
		/// <returns>The 1-based bar and the 1-based beat of bar.</returns>
		public (uint bar, float beat) TimeSpanToBarBeat(TimeSpan timeSpan) => BeatOnlyToBarBeat(TimeSpanToBeatOnly(timeSpan));
		private static float BarBeatToBeatOnly(uint bar, float beat, IEnumerable<SetCrotchetsPerBar> Collection)
		{
			(float BeatOnly, uint Bar, uint CPB) foreCPB = new(1f, 1U, 8U);
			SetCrotchetsPerBar? LastCPB = Collection.LastOrDefault((SetCrotchetsPerBar i) => i.Active && i.Beat.BarBeat.bar < bar);
			if (LastCPB != null)
				foreCPB = new(LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar);
			return foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1f;
		}
		private static (uint bar, float beat) BeatOnlyToBarBeat(float beat, IEnumerable<SetCrotchetsPerBar> Collection)
		{
			(float BeatOnly, uint Bar, uint CPB) foreCPB = new(1f, 1U, 8U);
			SetCrotchetsPerBar? LastCPB = Collection.LastOrDefault((SetCrotchetsPerBar i) => i.Active && i.Beat.BeatOnly < beat);
			if (LastCPB != null)
				foreCPB = new(LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar);
			(uint bar, float beat) result = ((uint)Math.Round(foreCPB.Bar + Math.Floor((double)((beat - foreCPB.BeatOnly) / foreCPB.CPB))), ((beat - foreCPB.BeatOnly) % foreCPB.CPB) + 1f);
			return result;
		}
		private static TimeSpan BeatOnlyToTimeSpan(float beatOnly, IEnumerable<BaseBeatsPerMinute> BPMCollection)
		{
			ValueTuple<float, float> fore = new(1f, 100f);
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
			ValueTuple<float, float> fore = new(1f, 100f);
			BaseBeatsPerMinute? foreBPM = BPMCollection.FirstOrDefault();
			if (foreBPM != null)
				fore = new ValueTuple<float, float>(foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute);
			float beatOnly = 1f;
			foreach (BaseBeatsPerMinute item in BPMCollection)
				if (timeSpan > BeatOnlyToTimeSpan(item.Beat.BeatOnly, BPMCollection))
					beatOnly = (float)((double)beatOnly + (BeatOnlyToTimeSpan(item.Beat.BeatOnly, BPMCollection) - BeatOnlyToTimeSpan(fore.Item1, BPMCollection)).TotalMinutes * (double)fore.Item2);
			beatOnly = (float)((double)beatOnly + (timeSpan - BeatOnlyToTimeSpan(fore.Item1, BPMCollection)).TotalMinutes * (double)fore.Item2);
			return beatOnly;
		}
		/// <summary>
		/// Creates a beat instance.
		/// </summary>
		public Beat BeatOf(float beatOnly) => new(this, beatOnly);
		/// <summary>
		/// Creates a beat instance.
		/// </summary>
		public Beat BeatOf(uint bar, float beat) => new(this, bar, beat);
		/// <summary>
		/// Creates a beat instance.
		/// </summary>
		public Beat BeatOf(TimeSpan timeSpan) => new(this, timeSpan);
		/// <summary>
		/// Calculate the BPM of the moment in which the beat is.
		/// </summary>
		public float BeatsPerMinuteOf(Beat beat) => _BPMList.LastOrDefault((BaseBeatsPerMinute i) => i.Beat < beat)?.BeatsPerMinute ?? 100f;
		/// <summary>
		/// Calculate the CPB of the moment in which the beat is.
		/// </summary>
		public float CrotchetsPerBarOf(Beat beat) => _CPBList.LastOrDefault((SetCrotchetsPerBar i) => i.Beat < beat)?.CrotchetsPerBar ?? 8;
		internal readonly RDLevel Collection;
		private List<BaseBeatsPerMinute> _BPMList = [];
		private List<SetCrotchetsPerBar> _CPBList = [];
	}
}
