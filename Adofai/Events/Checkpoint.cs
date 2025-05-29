namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents a checkpoint event in the ADOFAI game.
	/// </summary>
	public class Checkpoint : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.Checkpoint;		/// <summary>
		/// Gets or sets the tile offset for the checkpoint.
		/// </summary>
		public int TileOffset { get; set; }
	}
}
