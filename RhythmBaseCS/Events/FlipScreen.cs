using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class FlipScreen : BaseEvent, IRoomEvent
	{
		public FlipScreen()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.FlipScreen;
			Tab = Tabs.Actions;
		}

		public Room Rooms { get; set; }

		public bool FlipX { get; set; }

		public bool FlipY { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString()
		{
			bool flipX = FlipX;
			string result;
			if (flipX)
			{
				if (FlipY)
				{
					result = "X";
				}
				else
				{
					result = "^v";
				}
			}
			else
			{
				if (FlipY)
				{
					result = "<>";
				}
				else
				{
					result = "";
				}
			}
			return base.ToString() + string.Format(" {0}", result);
		}
	}
}
