using System;

namespace RhythmBase.Adofai.Events
{

	public abstract class ADBaseTaggedTileAction : ADBaseTileEvent
	{

		public float AngleOffset { get; set; }


		public string EventTag { get; set; }
	}
}
