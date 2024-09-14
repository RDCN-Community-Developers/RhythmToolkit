using System;

namespace RhythmBase.Adofai.Events
{

	public class ADPause : ADBaseTileEvent
	{

		public ADPause()
		{
			Type = ADEventType.Pause;
		}


		public override ADEventType Type { get; }


		public float Duration { get; set; }


		public int CountdownTicks { get; set; }


		public int AngleCorrectionDir { get; set; }
	}
}
