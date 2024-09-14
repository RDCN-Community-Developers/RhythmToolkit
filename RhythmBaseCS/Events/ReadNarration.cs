using System;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class ReadNarration : BaseEvent
	{

		public ReadNarration()
		{
			Type = EventType.ReadNarration;
			Tab = Tabs.Actions;
			Rooms = Room.Default();
		}


		public override EventType Type { get; }


		public string Text { get; set; }


		public NarrationCategory Category { get; set; }


		public override Tabs Tab { get; }


		public Room Rooms { get; set; }


		public override string ToString() => base.ToString() + string.Format(" {0}", Text);


		public enum NarrationCategory
		{

			Fallback,

			Navigation,

			Instruction,

			Notification,

			Dialogue,

			Description = 6,

			Subtitles
		}
	}
}
