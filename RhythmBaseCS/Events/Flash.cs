using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class Flash : BaseEvent
	{
		public Flash()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.Flash;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public Durations Duration { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", Duration);

		public enum Durations
		{
			Short,
			Medium,
			Long
		}
	}
}
