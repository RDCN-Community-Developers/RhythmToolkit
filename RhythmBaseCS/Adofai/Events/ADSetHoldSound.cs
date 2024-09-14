using System;

namespace RhythmBase.Adofai.Events
{

	public class ADSetHoldSound : ADBaseTileEvent
	{

		public ADSetHoldSound()
		{
			Type = ADEventType.SetHoldSound;
		}


		public override ADEventType Type { get; }


		public string HoldStartSound { get; set; }


		public string HoldLoopSound { get; set; }


		public string HoldEndSound { get; set; }


		public string HoldMidSound { get; set; }


		public string HoldMidSoundType { get; set; }


		public float HoldMidSoundDelay { get; set; }


		public string HoldMidSoundTimingRelativeTo { get; set; }


		public int HoldSoundVolume { get; set; }
	}
}
