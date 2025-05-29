using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents the Twirl event in the ADOFAI editor.  
	/// </summary>  
	public class Twirl : BaseTileEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.Twirl;
	}
}
