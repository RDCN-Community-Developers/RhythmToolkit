using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event that sets text in the Adofai level.  
	/// </summary>  
	public class ADSetText : ADBaseTaggedTileAction
	{		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.SetText;		/// <summary>  
		/// Gets or sets the text to be displayed in the level.  
		/// </summary>  
		public string DecText { get; set; } = string.Empty;		/// <summary>  
		/// Gets or sets the tag associated with the text event.  
		/// </summary>  
		public string Tag { get; set; } = string.Empty;
	}
}
