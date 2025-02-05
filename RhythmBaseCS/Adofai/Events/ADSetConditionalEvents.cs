using System;
namespace RhythmBase.Adofai.Events
{
	public class ADSetConditionalEvents : ADBaseTileEvent
	{
		public ADSetConditionalEvents()
		{
			Type = ADEventType.SetConditionalEvents;
		}

		public override ADEventType Type { get; }

		public string PerfectTag { get; set; }

		public string HitTag { get; set; }

		public string EarlyPerfectTag { get; set; }

		public string LatePerfectTag { get; set; }

		public string BarelyTag { get; set; }

		public string VeryEarlyTag { get; set; }

		public string VeryLateTag { get; set; }

		public string MissTag { get; set; }

		public string TooEarlyTag { get; set; }

		public string TooLateTag { get; set; }

		public string LossTag { get; set; }

		public string OnCheckpointTag { get; set; }
	}
}
