using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a move event in the rhythm base system.
/// </summary>
public class Move : BaseDecorationAction, IEaseEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.Move;
	///<inheritdoc/>
	public override Tab Tab => Tab.Decorations;

	/// <summary>
	/// Gets or sets the position of the move event.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Position)} is not null")]
	public RDPointE? Position { get; set; }
	/// <summary>
	/// Gets or sets the scale of the move event.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
	public RDSizeE? Scale { get; set; }
	/// <summary>
	/// Gets or sets the angle of the move event.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
	public RDExpression? Angle { get; set; }
	/// <summary>
	/// Gets or sets the pivot point of the move event.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Pivot)} is not null")]
	public RDPointE? Pivot { get; set; }
	///<inheritdoc/>
	public float Duration { get; set; } = 1;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	///<inheritdoc/>
	public override string ToString() => base.ToString();
}
