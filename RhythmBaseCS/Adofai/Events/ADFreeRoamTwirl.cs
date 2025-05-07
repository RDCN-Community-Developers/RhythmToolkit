using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents the Free Roam Twirl event in the ADOFAI editor.  
	/// </summary>  
	public class ADFreeRoamTwirl : ADBaseTileEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.FreeRoamTwirl;		/// <summary>  
		/// Gets or sets the position associated with the Free Roam Twirl event.  
		/// </summary>  
		public int Position { get; set; }
	}
}
