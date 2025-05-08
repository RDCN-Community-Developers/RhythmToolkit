using System;
using RhythmBase.Components;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a screen scroll action in the Adofai event system.  
	/// </summary>  
	public class ScreenScroll : BaseTaggedTileAction, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ScreenScroll;		/// <summary>  
		/// Gets or sets the scroll size for the screen.  
		/// </summary>  
		public RDSizeN Scroll { get; set; }
	}
}
