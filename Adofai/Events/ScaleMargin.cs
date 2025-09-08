namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the ScaleMargin event, which adjusts the margin scaling in the level.
	/// </summary>
	public class ScaleMargin : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ScaleMargin;		/// <summary>
		/// Gets or sets the scale value for the margin adjustment.
		/// </summary>
		public int Scale { get; set; }
	}
}
