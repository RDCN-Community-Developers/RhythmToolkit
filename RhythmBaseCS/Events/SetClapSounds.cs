using System;
using RhythmBase.Assets;
namespace RhythmBase.Events
{
	public class SetClapSounds : BaseEvent
	{
		public SetClapSounds()
		{
			Type = EventType.SetClapSounds;
			Tab = Tabs.Sounds;
		}

		public Audio P1Sound { get; set; }

		public Audio P2Sound { get; set; }

		public Audio CpuSound { get; set; }

		public RowType RowType { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }
	}
}
