using System;
namespace RhythmBase.Events
{
	public class SetHeartExplodeInterval : BaseEvent
	{
		public SetHeartExplodeInterval()
		{
			Type = EventType.SetHeartExplodeInterval;
			Tab = Tabs.Sounds;
		}

		public IntervalTypes IntervalType { get; set; }

		public int Interval { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public enum IntervalTypes
		{
			OneBeatAfter,
			Instant,
			GatherNoCeil,
			GatherAndCeil
		}
	}
}
