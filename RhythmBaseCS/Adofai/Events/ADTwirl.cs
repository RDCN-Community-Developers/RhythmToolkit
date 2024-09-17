using System;
namespace RhythmBase.Adofai.Events
{
	public class ADTwirl : ADBaseTileEvent
	{
		public ADTwirl()
		{
			Type = ADEventType.Twirl;
		}

		public override ADEventType Type { get; }
	}
}
