namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that sets text in the Adofai level.  
	/// </summary>  
	public class SetText : BaseTaggedTileAction, IStartEvent
	{		/// <inheritdoc/>
		public override EventType Type => EventType.SetText;		/// <summary>  
		/// Gets or sets the text to be displayed in the level.  
		/// </summary>  
		public string DecText { get; set; } = string.Empty;		/// <summary>  
		/// Gets or sets the tag associated with the text event.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;
	}
}
