using System;

namespace RhythmBase.Events
{

	public class SetPlayStyle : BaseEvent
	{

		public SetPlayStyle()
		{
			Type = EventType.SetPlayStyle;
			Tab = Tabs.Actions;
		}


		public PlayStyles PlayStyle { get; set; }


		public int NextBar { get; set; }


		public bool Relative { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public enum PlayStyles
		{

			Normal,

			Loop,

			Prolong,

			Immediately,

			ExtraImmediately,

			ProlongOneBar
		}
	}
}
