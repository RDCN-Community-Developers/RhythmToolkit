using System;
namespace RhythmBase.Adofai.Events
{
	public class ADScaleMargin : ADBaseTileEvent
	{
		public ADScaleMargin()
		{
			Type = ADEventType.ScaleMargin;
		}

		public override ADEventType Type { get; }

		public int Scale { get; set; }
	}
}
