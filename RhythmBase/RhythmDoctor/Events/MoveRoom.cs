using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to move a room with easing properties.
/// </summary>
[RDJsonObjectSerializable]
public record class MoveRoom : BaseEvent, IEaseEvent
{
	/// <summary>
	/// Gets or sets the position of the room.
	/// </summary>
	/// <remarks>
	/// Percentage of the screen width and height. (0,0) is the bottom-left corner, (100,100) is the top-right corner.
	/// Leave it null to keep the original position.
	/// </remarks>
	[Tween]
	[RDJsonAlias("roomPosition")]
	[RDJsonCondition($"$&.{nameof(Position)} is not null")]
	public RDPoint? Position { get; set; }
	/// <summary>
	/// Gets or sets the scale of the room.
	/// </summary>
	/// <remarks>
	/// Percentage of the original size. (100,100) is the original size.
	/// Leave it null to keep the original size.
	/// </remarks>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
	public RDSize? Scale { get; set; } = null;
	/// <summary>
	/// Gets or sets the angle of the room.
	/// </summary>
	/// <remarks>
	/// Degree. (0) is the original angle.
	/// Leave it null to keep the original angle.
	/// </remarks>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
	public float? Angle { get; set; } = null;
	/// <summary>
	/// Gets or sets the pivot point of the room.
	/// </summary>
	/// <remarks>
	/// Percentage of the original size. (0,0) is the bottom-left corner, (100,100) is the top-right corner.
	/// Leave it null to keep the original pivot point.
	/// </remarks>
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
}
