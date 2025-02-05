using System;
namespace RhythmBase.Adofai.Events
{
	public class ADMultiPlanet : ADBaseTileEvent
	{
		public ADMultiPlanet()
		{
			Type = ADEventType.MultiPlanet;
		}

		public override ADEventType Type { get; }

		public string Planets { get; set; }
	}
}
