using System;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a screen tile action in the Adofai event system.  
	/// </summary>  
	public class ADScreenTile : ADBaseTaggedTileAction, IEaseEvent
	{		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.ScreenTile;		/// <summary>  
		/// Gets or sets the duration of the screen tile effect.  
		/// </summary>  
		public float Duration { get; set; }		/// <summary>  
		/// Gets or sets the tile position for the screen tile effect.  
		/// </summary>  
		public RDPoint Tile { get; set; }		/// <summary>  
		/// Gets or sets the easing type for the screen tile effect.  
		/// </summary>  
		public EaseType Ease { get; set; }
	}
}
