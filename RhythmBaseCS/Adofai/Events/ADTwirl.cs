using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents the Twirl event in the ADOFAI editor.  
	/// </summary>  
	public class ADTwirl : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.Twirl;
	}
}
