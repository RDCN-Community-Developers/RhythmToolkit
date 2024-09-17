using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class ShakeScreen : BaseEvent, IRoomEvent
	{
		public ShakeScreen()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.ShakeScreen;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public ShakeLevels ShakeLevel { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", ShakeLevel);

		public enum ShakeLevels
		{
			Low,
			Medium,
			High
		}
	}
}
