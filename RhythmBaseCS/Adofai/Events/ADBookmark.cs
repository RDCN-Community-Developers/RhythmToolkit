using System;
namespace RhythmBase.Adofai.Events
{
	public class ADBookmark : ADBaseTileEvent
	{
		public ADBookmark()
		{
			Type = ADEventType.Bookmark;
		}

		public override ADEventType Type { get; }
	}
}
