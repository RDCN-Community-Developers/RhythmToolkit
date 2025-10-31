namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents the Twirl event in the ADOFAI editor.  
	/// </summary>  
	public class Twirl : BaseTileEvent, ISingleEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.Twirl;
	}
}
