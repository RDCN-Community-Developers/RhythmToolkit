using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class Stutter : BaseEvent, IRoomEvent
	{
		public Stutter()
		{
			Rooms = new Room(false, new byte[1]);
			Type = EventType.Stutter;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public float SourceBeat { get; set; }

		public float Length { get; set; }

		public Actions Action { get; set; }

		public int Loops { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public enum Actions
		{
			Add,
			Cancel
		}
	}
}
