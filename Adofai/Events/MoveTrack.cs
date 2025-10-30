using RhythmBase.Adofai.Components;
using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events;

/// <summary>  
/// Represents an event to move a track in the Adofai editor.  
/// </summary>  
public class MoveTrack : BaseTaggedTileEvent, IEaseEvent, IBeginningEvent
{
	/// <inheritdoc/>  
	public override EventType Type => EventType.MoveTrack;
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
	[RDJsonCondition($"$&.{nameof(PositionOffset)} is not null")]
	public RDPoint? PositionOffset { get; set; }
	[RDJsonCondition($"$&.{nameof(RotationOffset)} is not null")]
	public float? RotationOffset { get; set; }
	[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
	public RDSize? Scale { get; set; }
	[RDJsonCondition($"$&.{nameof(Opacity)} is not null")]
	public float? Opacity { get; set; }
	/// <summary>  
	/// Gets or sets the easing type for the track movement.  
	/// </summary>  
	public EaseType Ease { get; set; }
	/// <summary>  
	/// Gets or sets a value indicating whether the movement should only affect the maximum visual effects.  
	/// </summary>  
	public bool MaxVfxOnly { get; set; }
}
