﻿using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to move a room with easing properties.
	/// </summary>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
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
		[EaseProperty]
		public RDPointE? RoomPosition { get; set; }
		/// <summary>
		/// Gets or sets the scale of the room.
		/// </summary>
		[EaseProperty]
		public RDSizeE? Scale { get; set; }
		/// <summary>
		/// Gets or sets the angle of the room.
		/// </summary>
		[EaseProperty]
		public RDExpression? Angle { get; set; }
		/// <summary>
		/// Gets or sets the pivot point of the room.
		/// </summary>
		[EaseProperty]
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
		[JsonIgnore]
		public RDRoom Rooms => new RDSingleRoom(checked((byte)Y));
	}
}
