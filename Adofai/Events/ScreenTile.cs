using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a screen tile action in the Adofai event system.  
	/// </summary>  
	public class ScreenTile : BaseTaggedTileAction, IEaseEvent, IStartEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ScreenTile;
		/// <summary>  
		/// Gets or sets the duration of the screen tile effect.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the tile position for the screen tile effect.  
		/// </summary>  
		public RDPoint Tile { get; set; }
		/// <summary>  
		/// Gets or sets the easing type for the screen tile effect.  
		/// </summary>  
		public EaseType Ease { get; set; }
	}
}
