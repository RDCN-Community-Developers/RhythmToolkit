using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class PulseCamera : BaseEvent, IRoomEvent
	{
		public PulseCamera()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.PulseCamera;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public byte Strength { get; set; }

		public int Count { get; set; }

		public float Frequency { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0},{1},{2}", Strength, Count, Frequency);
	}
}
