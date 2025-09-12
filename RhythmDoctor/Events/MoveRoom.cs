using RhythmBase.Global.Components.Easing;
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
		public MoveRoom()
		{
			Type = EventType.MoveRoom;
			Tab = Tabs.Rooms;
		}
		/// <summary>
		/// Gets or sets the position of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition("$&.RoomPosition is not null")]
		public RDPointE? RoomPosition { get; set; }
		/// <summary>
		/// Gets or sets the scale of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition("$&.Scale is not null")]
		public RDSizeE? Scale { get; set; }
		/// <summary>
		/// Gets or sets the angle of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition("$&.Angle is not null")]
		public RDExpression? Angle { get; set; }
		/// <summary>
		/// Gets or sets the pivot point of the room.
		/// </summary>
		[Tween]
		[RDJsonCondition("$&.Pivot is not null")]
		public RDPointE? Pivot { get; set; }
		/// <summary>
		/// Gets or sets the duration of the move event.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the easing type of the move event.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
		/// <summary>
		/// Gets the room associated with the event.
		/// </summary>
		public RDRoom Rooms => new RDSingleRoom(checked((byte)Y));
	}
}
