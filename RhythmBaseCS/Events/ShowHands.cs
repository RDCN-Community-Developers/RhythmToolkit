using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class ShowHands : BaseEvent, IRoomEvent
	{
		public ShowHands()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.ShowHands;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public Actions Action { get; set; }

		public PlayerHands Hand { get; set; }

		public bool Align { get; set; }

		public bool Instant { get; set; }

		public Extents Extent { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public enum Actions
		{
			Show,
			Hide,
			Raise,
			Lower
		}

		public enum Extents
		{
			Full,
			Short
		}
	}
}
