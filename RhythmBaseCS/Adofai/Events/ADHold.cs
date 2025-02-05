using System;
namespace RhythmBase.Adofai.Events
{
	public class ADHold : ADBaseTileEvent
	{
		public ADHold()
		{
			Type = ADEventType.Hold;
		}

		public override ADEventType Type { get; }

		public int Duration { get; set; }

		public int DistanceMultiplier { get; set; }

		public bool LandingAnimation { get; set; }
	}
}
