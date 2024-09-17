using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class SetHandOwner : BaseEvent, IRoomEvent
	{
		public SetHandOwner()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.SetHandOwner;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public PlayerHands Hand { get; set; }

		public string Character { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public enum Characters
		{
			Players,
			Ian,
			Paige,
			Edega
		}
	}
}
