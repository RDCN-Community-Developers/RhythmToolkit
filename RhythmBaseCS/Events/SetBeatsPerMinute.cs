using System;
namespace RhythmBase.Events
{
	public class SetBeatsPerMinute : BaseBeatsPerMinute
	{
		public SetBeatsPerMinute()
		{
			Type = EventType.SetBeatsPerMinute;
			Tab = Tabs.Sounds;
		}

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" BPM:{0}", BeatsPerMinute);
	}
}
