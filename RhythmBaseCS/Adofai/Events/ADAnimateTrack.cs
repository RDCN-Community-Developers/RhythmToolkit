using System;
using Newtonsoft.Json;
namespace RhythmBase.Adofai.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADAnimateTrack : ADBaseTileEvent
	{
		public ADAnimateTrack()
		{
			Type = ADEventType.AnimateTrack;
		}

		public override ADEventType Type { get; }

		public TrackAnimations? TrackAnimation { get; set; }

		public TrackDisappearAnimations? TrackDisappearAnimation { get; set; }

		public int BeatsAhead { get; set; }

		public int BeatsBehind { get; set; }

		public enum TrackAnimations
		{
			None,
			Assemble,
			Assemble_Far,
			Extend,
			Grow,
			Grow_Spin,
			Fade,
			Drop,
			Rise
		}

		public enum TrackDisappearAnimations
		{
			None,
			Scatter,
			Scatter_Far,
			Retract,
			Shrink,
			Shrink_Spin,
			Fade
		}
	}
}
