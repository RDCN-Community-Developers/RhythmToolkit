using System;
namespace RhythmBase.Adofai.Events
{
	public class ADRepeatEvents : ADBaseTileEvent
	{
		public ADRepeatEvents()
		{
			Type = ADEventType.RepeatEvents;
		}

		public override ADEventType Type { get; }

		public RepeatTypes RepeatType { get; set; }

		public int Repetitions { get; set; }

		public int FloorCount { get; set; }

		public float Interval { get; set; }

		public bool ExecuteOnCurrentFloor { get; set; }

		public string Tag { get; set; }

		public enum RepeatTypes
		{
			Beat,
			Floor
		}
	}
}
