using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class SetRoomContentMode : BaseEvent
	{
		public SetRoomContentMode()
		{
			Type = EventType.SetRoomContentMode;
			Tab = Tabs.Rooms;
		}

		public string Mode { get; set; }

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

		public enum Modes
		{
			Center,
			AspectFill
		}
	}
}
