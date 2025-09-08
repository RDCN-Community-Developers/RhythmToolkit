namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents a bookmark event in the ADOFAI game.
	/// </summary>
	public class Bookmark : BaseTileEvent, IStartEvent
	{
		/// <inheritdoc/>s
		public override EventType Type => EventType.Bookmark;
	}
}