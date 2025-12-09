using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a tile event in the rhythm base system.
/// </summary>
public class Tile : BaseDecorationAction, IEaseEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.Tile;
	///<inheritdoc/>
	public override Tab Tab => Tab.Decorations;

	/// <summary>
	/// Gets or sets the position of the tile.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Position)} is not null")]
	public RDPoint? Position { get; set; }
	/// <summary>
	/// Gets or sets the tiling of the tile.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Tiling)} is not null")]
	public RDPoint? Tiling { get; set; }
	/// <summary>
	/// Gets or sets the speed of the tile.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Speed)} is not null")]
	public RDPoint? Speed { get; set; }
	/// <summary>
	/// Gets or sets the type of tiling.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Speed)} is not null")]
	public TilingTypes TilingType { get; set; } = TilingTypes.Scroll;
	/// <summary>
	/// Gets or sets the interval for the tiling.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(TilingType)} is RhythmBase.RhythmDoctor.Events.TilingTypes.Pulse")]
	public float Interval { get; set; } = 0.01f;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	///<inheritdoc/>
	public float Duration { get; set; } = 0f;
}
