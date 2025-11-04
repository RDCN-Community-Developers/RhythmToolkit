using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to move a room with easing properties.
	/// </summary>
	public class MoveRoom : BaseEvent, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveRoom"/> class.
		/// </summary>
		public MoveRoom() { }
		/// <summary>
		/// Gets or sets the position of the room.
		/// </summary>
		[Tween]
		[RDJsonProperty("roomPosition")]
		[RDJsonCondition($"$&.{nameof(Position)} is not null")]
		public RDPointE? Position { get; set; }
		/// <summary>
		/// Gets or sets the scale of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
		public RDSizeE? Scale { get; set; } = null;
		/// <summary>
		/// Gets or sets the angle of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
		public RDExpression? Angle { get; set; } = null;
		/// <summary>
		/// Gets or sets the pivot point of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Pivot)} is not null")]
		public RDPointE? Pivot { get; set; } = null;
		/// <summary>
		/// Gets or sets the duration of the move event.
		/// </summary>
		public float Duration { get; set; } = 1;
		/// <summary>
		/// Gets or sets the easing type of the move event.
		/// </summary>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.MoveRoom;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Rooms;
		/// <summary>
		/// Gets the room associated with the event.
		/// </summary>
		public RDRoom Rooms => new RDSingleRoom(checked((byte)Y));
	}
}
