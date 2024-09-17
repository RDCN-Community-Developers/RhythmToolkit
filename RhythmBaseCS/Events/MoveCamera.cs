using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveCamera : BaseEvent, IEaseEvent, IRoomEvent
	{
		public MoveCamera()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.MoveCamera;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		[EaseProperty]
		public PointE? CameraPosition { get; set; }

		[EaseProperty]
		public int? Zoom { get; set; }

		[EaseProperty]
		public Expression? Angle { get; set; }

		public float Duration { get; set; }

		public Ease.EaseType Ease { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }
	}
}
