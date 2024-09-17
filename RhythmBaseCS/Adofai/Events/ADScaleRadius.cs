using System;
namespace RhythmBase.Adofai.Events
{
	public class ADScaleRadius : ADBaseTileEvent
	{
		public ADScaleRadius()
		{
			Type = ADEventType.ScaleRadius;
		}

		public override ADEventType Type { get; }

		public int Scale { get; set; }
	}
}
