using System;
namespace RhythmBase.Adofai.Events
{
	public class ADHallOfMirrors : ADBaseTaggedTileAction
	{
		public ADHallOfMirrors()
		{
			Type = ADEventType.HallOfMirrors;
		}

		public override ADEventType Type { get; }

		public bool Enabled { get; set; }
	}
}
