using RhythmBase.Adofai.Components;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
using System.Runtime.CompilerServices;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to move a track in the Adofai editor.  
	/// </summary>  
	public class ADMoveTrack : ADBaseTaggedTileAction, IEaseEvent
	{
		/// <inheritdoc/>  
		public override ADEventType Type => ADEventType.MoveTrack;
		/// <summary>  
		/// Gets or sets the starting tile for the track movement.  
		/// </summary>  
		public TileReference StartTile { get; set; }
		/// <summary>  
		/// Gets or sets the reference to the ending tile for the track movement.  
		/// </summary>  
		public TileReference EndTile { get; set; }
		/// <summary>  
		/// Gets or sets the gap length between tiles.  
		/// </summary>  
		public int GapLength { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the track movement.  
		/// </summary>  
		public float Duration { get; set; }
		/// <summary>  
		/// Gets or sets the position offset for the track movement.  
		/// </summary>  
		public RDPoint PositionOffset { get; set; }		/// <summary>  
		/// Gets or sets the easing type for the track movement.  
		/// </summary>  
		public EaseType Ease { get; set; }		/// <summary>  
		/// Gets or sets a value indicating whether the movement should only affect the maximum visual effects.  
		/// </summary>  
		public bool MaxVfxOnly { get; set; }	}
}
