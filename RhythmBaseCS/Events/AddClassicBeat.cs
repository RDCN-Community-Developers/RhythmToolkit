using System;
using Newtonsoft.Json;
using RhythmBase.Extensions;
using RhythmBase.Utils;
namespace RhythmBase.Events
{
	public class AddClassicBeat : BaseBeat
	{
		public AddClassicBeat()
		{
			Tick = 1f;
			Type = EventType.AddClassicBeat;
		}

		public float Tick { get; set; }

		public float Swing { get; set; }

		public float Hold { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public ClassicBeatPatterns SetXs { get; set; }

		public override EventType Type { get; }

		public override string ToString() => string.Format("{0} {1} {2}", base.ToString(), Utils.Utils.GetPatternString(this.RowXs()), ((double)Swing == 0.5 | Swing == 0f) ? "" : " Swing");

		public enum ClassicBeatPatterns
		{
			NoChange,
			ThreeBeat,
			FourBeat
		}
	}
}
