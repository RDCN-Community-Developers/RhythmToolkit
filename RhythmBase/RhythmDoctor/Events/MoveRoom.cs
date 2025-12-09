using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to move a room with easing properties.
/// </summary>
public class MoveRoom : BaseEvent, IEaseEvent
{
	/// <summary>
	/// Gets or sets the position of the room.
	/// </summary>
	[Tween]
	[RDJsonProperty("roomPosition")]
	[RDJsonCondition($"$&.{nameof(Position)} is not null")]
	public RDPoint? Position { get; set; }
	/// <summary>
	/// Gets or sets the scale of the room.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
	public RDSize? Scale { get; set; } = null;
	/// <summary>
	/// Gets or sets the angle of the room.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
	public float? Angle { get; set; } = null;
	/// <summary>
	/// Gets or sets the pivot point of the room.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Pivot)} is not null")]
	public RDPoint? Pivot { get; set; } = null;
	///<inheritdoc/>
	public float Duration { get; set; } = 1;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	///<inheritdoc/>
	public override EventType Type => EventType.MoveRoom;
	///<inheritdoc/>
	public override Tab Tab => Tab.Rooms;

	/// <summary>
	/// Gets the room associated with the event.
	/// </summary>
	public RDRoom Rooms => new RDSingleRoom(checked((byte)Y));
}
