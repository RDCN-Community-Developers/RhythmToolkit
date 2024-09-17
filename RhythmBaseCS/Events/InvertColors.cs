using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class InvertColors : BaseEvent, IRoomEvent
	{
		public InvertColors()
		{
			Rooms = new Room(false, new byte[1]);
			Type = EventType.InvertColors;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public bool Enable { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", Enable);
	}
}
