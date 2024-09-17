using System;
namespace RhythmBase.Adofai.Events
{
	public class ADCheckpoint : ADBaseTileEvent
	{
		public ADCheckpoint()
		{
			Type = ADEventType.Checkpoint;
		}

		public override ADEventType Type { get; }

		public int TileOffset { get; set; }
	}
}
