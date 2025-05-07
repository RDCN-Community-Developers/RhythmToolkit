namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an editor comment event in ADOFAI.  
	/// </summary>  
	public class ADEditorComment : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.EditorComment;		/// <summary>  
		/// Gets or sets the comment associated with this event.  
		/// </summary>  
		public string Comment { get; set; } = string.Empty;
	}
}