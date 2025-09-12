using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components.Timeline
{
	public enum OneshotPulseState
	{
		None,

		Freezing,
		Frozen,
		Unfreezing,

		Burning,
		Burned,
		Unburning,
	}
	public abstract record class BeatTimeline
	{
		public RowTypes RowType { get; internal set; }
		public Curve<int> HitIndex { get; internal set; }
		public Curve<bool> IsHolding { get; internal set; }
	}
	public record class ClassicBeatTimeline : BeatTimeline
	{
		public Curve<int> BeatIndex { get; internal set; }
		public Curve<Patterns[]> Patterns { get; internal set; }
	}
	public record class OneshotBeatTimeline : BeatTimeline
	{
		public Curve<OneshotPulseShapeTypes> PulseType { get; internal set; }
		public Curve<int> BeatCount { get; internal set; }
		public Curve<bool> IsSkipping { get; internal set; }
		public Curve<OneshotPulseState> PulseState { get; internal set; }
	}
}
