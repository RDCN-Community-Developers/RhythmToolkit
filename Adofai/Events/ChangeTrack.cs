using RhythmBase.Adofai.Components;

namespace RhythmBase.Adofai.Events
{
	internal class ChangeTrack : BaseTileEvent
	{
		public override EventType Type => EventType.ChangeTrack;
		public TrackColorType TrackColorType { get; set; } = TrackColorType.Single;
		public RDColor TrackColor { get; set; } = RDColor.FromRgba("debb7b");
		public RDColor SecondaryTrackColor { get; set; } = RDColor.White;
		public float TrackColorAnimDuration { get; set; } = 2;
		public TrackColorPulse TrackColorPulse { get; set; } = TrackColorPulse.None;
		public int TrackPulseLength { get; set; } = 10;
		public TrackStyle TrackStyle { get; set; } = TrackStyle.Standard;
		public TrackAnimationType TrackAnimation { get; set; } = TrackAnimationType.None;
		public float BeatsAhead { get; set; } = 3;
		public TrackDisappearAnimationType TrackDisappearAnimation { get; set; } = TrackDisappearAnimationType.None;
		public float BeatsBehind { get; set; } = 4;
	}
}
