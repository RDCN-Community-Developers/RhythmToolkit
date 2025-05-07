using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents a bookmark event in the ADOFAI game.
	/// </summary>
	public class ADBookmark : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.Bookmark;
	}
}
