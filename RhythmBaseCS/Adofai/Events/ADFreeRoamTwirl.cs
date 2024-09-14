using System;

namespace RhythmBase.Adofai.Events
{

	public class ADFreeRoamTwirl : ADBaseTileEvent
	{

		public ADFreeRoamTwirl()
		{
			Type = ADEventType.FreeRoamTwirl;
		}


		public override ADEventType Type { get; }


		public int Position { get; set; }
	}
}
