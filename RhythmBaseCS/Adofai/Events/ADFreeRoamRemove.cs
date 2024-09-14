using System;

namespace RhythmBase.Adofai.Events
{

	public class ADFreeRoamRemove : ADBaseTileEvent
	{

		public override ADEventType Type { get; }


		public int Position { get; set; }


		public int Size { get; set; }
	}
}
