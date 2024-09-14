using System;

namespace RhythmBase.Adofai.Events
{

	public class ADSetHitsound : ADBaseTileEvent
	{

		public ADSetHitsound()
		{
			Type = ADEventType.SetHitsound;
		}


		public override ADEventType Type { get; }


		public string GameSound { get; set; }


		public string Hitsound { get; set; }


		public int HitsoundVolume { get; set; }


		public enum GameSounds
		{

			Hitsound,

			Midspin
		}
	}
}
