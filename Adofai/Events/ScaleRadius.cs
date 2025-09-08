namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the ScaleRadius event in the ADOFAI editor.
	/// </summary>
	public class ScaleRadius : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ScaleRadius;		/// <summary>
		/// Gets or sets the scale value for the radius adjustment.
		/// </summary>
		public int Scale { get; set; }
	}
}
