using System;
namespace RhythmBase.Adofai.Events
{
	public abstract class ADBaseEvent
	{
		public abstract ADEventType Type { get; }

		public override string ToString() => Type.ToString();
	}
}
