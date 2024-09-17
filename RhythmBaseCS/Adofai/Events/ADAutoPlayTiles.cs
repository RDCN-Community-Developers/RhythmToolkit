using System;
namespace RhythmBase.Adofai.Events
{
	public class ADAutoPlayTiles : ADBaseTileEvent
	{
		public ADAutoPlayTiles()
		{
			Type = ADEventType.AutoPlayTiles;
		}

		public override ADEventType Type { get; }

		public bool Enabled { get; set; }

		public bool ShowStatusText { get; set; }

		public bool SafetyTiles { get; set; }
	}
}
