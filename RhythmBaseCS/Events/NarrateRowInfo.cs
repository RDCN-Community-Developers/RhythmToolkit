using System;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class NarrateRowInfo : BaseRowAction, IRoomEvent
	{

		public NarrateRowInfo()
		{
			Type = EventType.NarrateRowInfo;
			Tab = Tabs.Actions;
			CustomPattern = new LimitedList<Patterns>(6U);
		}


		public override EventType Type { get; }


		public Room Rooms { get; set; }


		public override Tabs Tab { get; }


		public NarrateInfoType InfoType { get; set; }


		public bool SoundOnly { get; set; }


		public string NarrateSkipBeats { get; set; }


		public LimitedList<Patterns> CustomPattern { get; set; }


		public bool SkipsUnstable { get; set; }


		public override string ToString() => base.ToString() + string.Format(" {0}:{1}", InfoType, NarrateSkipBeats);


		public enum NarrateInfoType
		{

			Connect,

			Update,

			Disconnect,

			Online,

			Offline
		}
	}
}
