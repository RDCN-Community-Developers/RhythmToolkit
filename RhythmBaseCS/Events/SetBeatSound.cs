using System;
using RhythmBase.Assets;

namespace RhythmBase.Events
{

	public class SetBeatSound : BaseRowAction
	{

		public SetBeatSound()
		{
			Sound = new Audio();
			Type = EventType.SetBeatSound;
			Tab = Tabs.Sounds;
		}


		public Audio Sound { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }
	}
}
