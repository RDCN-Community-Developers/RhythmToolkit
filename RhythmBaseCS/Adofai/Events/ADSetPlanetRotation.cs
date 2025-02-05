using System;
namespace RhythmBase.Adofai.Events
{
	public class ADSetPlanetRotation : ADBaseTileEvent
	{
		public ADSetPlanetRotation()
		{
			Type = ADEventType.SetPlanetRotation;
		}

		public override ADEventType Type { get; }

		public string Ease { get; set; }

		public int EaseParts { get; set; }

		public ADEasePartBehaviors EasePartBehavior { get; set; }
	}
}
