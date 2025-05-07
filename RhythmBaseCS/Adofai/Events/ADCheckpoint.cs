namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents a checkpoint event in the ADOFAI game.
	/// </summary>
	public class ADCheckpoint : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.Checkpoint;		/// <summary>
		/// Gets or sets the tile offset for the checkpoint.
		/// </summary>
		public int TileOffset { get; set; }
	}
}
