using System;

namespace RhythmBase.Adofai.Events
{

	public class ADPlaySound : ADBaseTaggedTileAction
	{

		public ADPlaySound()
		{
			Type = ADEventType.PlaySound;
		}


		public override ADEventType Type { get; }


		public string Hitsound { get; set; }


		public int HitsoundVolume { get; set; }
	}
}
