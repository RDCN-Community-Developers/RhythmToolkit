using System;
namespace RhythmBase.Events
{
	public class AddFreeTimeBeat : BaseBeat
	{
		public AddFreeTimeBeat()
		{
			Type = EventType.AddFreeTimeBeat;
		}

		public float Hold { get; set; }

		public byte Pulse { get; set; }

		public override EventType Type { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", (int)checked(Pulse + 1));
	}
}
