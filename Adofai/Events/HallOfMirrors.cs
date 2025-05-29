namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents the Hall of Mirrors event in the Adofai event system.  
	/// </summary>  
	public class HallOfMirrors : BaseTaggedTileAction, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.HallOfMirrors;
		/// <summary>  
		/// Gets or sets a value indicating whether the Hall of Mirrors effect is enabled.  
		/// </summary>  
		public bool Enabled { get; set; }
	}
}
