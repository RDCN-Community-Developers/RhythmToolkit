using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class BassDrop : BaseEvent, IRoomEvent
	{
		public BassDrop()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.BassDrop;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public StrengthType Strength { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", Strength);

		public enum StrengthType
		{
			Low,
			Medium,
			High
		}
	}
}
