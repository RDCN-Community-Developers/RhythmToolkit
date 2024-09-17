using System;
namespace RhythmBase.Events
{
	public class FinishLevel : BaseEvent
	{
		public FinishLevel()
		{
			Type = EventType.FinishLevel;
			Tab = Tabs.Actions;
		}

		public override EventType Type { get; }

		public override Tabs Tab { get; }
	}
}
