using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveRoom : BaseEvent, IEaseEvent
	{
		public MoveRoom()
		{
			Type = EventType.MoveRoom;
			Tab = Tabs.Rooms;
		}

		[EaseProperty]
		public PointE? RoomPosition { get; set; }

		[EaseProperty]
		public RDSizeE? Scale { get; set; }

		[EaseProperty]
		public Expression? Angle { get; set; }

		[EaseProperty]
		public PointE? Pivot { get; set; }

		public float Duration { get; set; }

		public Ease.EaseType Ease { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		[JsonIgnore]
		public Room Rooms
		{
			get
			{
				return new SingleRoom(checked((byte)Y));
			}
		}
	}
}
