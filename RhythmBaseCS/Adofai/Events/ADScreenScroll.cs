using System;
using RhythmBase.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a screen scroll action in the Adofai event system.  
	/// </summary>  
	public class ADScreenScroll : ADBaseTaggedTileAction
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.ScreenScroll;		/// <summary>  
		/// Gets or sets the scroll size for the screen.  
		/// </summary>  
		public RDSizeN Scroll { get; set; }
	}
}
