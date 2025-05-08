namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents an event that enables or disables auto-playing tiles in the game.
	/// </summary>
	public class AutoPlayTiles : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.AutoPlayTiles;		/// <summary>
		/// Gets or sets a value indicating whether auto-play is enabled for tiles.
		/// </summary>
		public bool Enabled { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether to show status text during auto-play.
		/// </summary>
		public bool ShowStatusText { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether safety tiles are enabled during auto-play.
		/// </summary>
		public bool SafetyTiles { get; set; }
	}
}
