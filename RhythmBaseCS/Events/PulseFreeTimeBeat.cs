using System;
using Microsoft.VisualBasic.CompilerServices;

namespace RhythmBase.Events
{

	public class PulseFreeTimeBeat : BaseBeat
	{

		public PulseFreeTimeBeat()
		{
			Type = EventType.PulseFreeTimeBeat;
		}


		public float Hold { get; set; }


		public ActionType Action { get; set; }


		public uint CustomPulse { get; set; }


		public override EventType Type { get; }


		public override string ToString()
		{
			string Out = "";
			switch (this.Action)
			{
				case ActionType.Increment:
					Out = ">";
					break;
				case ActionType.Decrement:
					Out = "<";
					break;
				case ActionType.Custom:
					Out = Conversions.ToString((long)checked(unchecked((ulong)this.CustomPulse) + 1UL));
					break;
				case ActionType.Remove:
					Out = "X";
					break;
			}
			return base.ToString() + string.Format(" {0}", Out);
		}


		public enum ActionType
		{

			Increment,

			Decrement,

			Custom,

			Remove
		}
	}
}
