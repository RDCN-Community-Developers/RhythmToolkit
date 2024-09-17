using System;
namespace RhythmBase.Adofai.Events
{
	public class ADSetSpeed : ADBaseTaggedTileAction
	{
		public ADSetSpeed()
		{
			Type = ADEventType.SetSpeed;
		}

		public override ADEventType Type { get; }

		public SpeedTypes SpeedType { get; set; }

		public float BeatsPerMinute { get; set; }

		public float BpmMultiplier { get; set; }

		public enum SpeedTypes
		{
			Bpm,
			Multiplier
		}
	}
}
