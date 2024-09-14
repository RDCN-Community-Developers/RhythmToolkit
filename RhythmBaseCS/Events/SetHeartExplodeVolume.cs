using System;

namespace RhythmBase.Events
{

	public class SetHeartExplodeVolume : BaseEvent, IBarBeginningEvent
	{

		public SetHeartExplodeVolume()
		{
			Type = EventType.SetHeartExplodeVolume;
			Tab = Tabs.Sounds;
		}


		public uint Volume { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }
	}
}
