using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class SetRoomPerspective : BaseEvent, IEaseEvent
	{
		public SetRoomPerspective()
		{
			CornerPositions = new List<RDPointE?>(4);
			Type = EventType.SetRoomPerspective;
			Tab = Tabs.Rooms;
		}

		[EaseProperty]
		public List<RDPointE?> CornerPositions { get; set; }

		public float Duration { get; set; }

		public Ease.EaseType Ease { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		[JsonIgnore]
		public Room Room
		{
			get
			{
				return new SingleRoom(checked((byte)Y));
			}
		}
	}
}
