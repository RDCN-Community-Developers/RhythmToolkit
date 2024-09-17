using System;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	public class ADFreeRoam : ADBaseTileEvent, IEaseEvent
	{
		public ADFreeRoam()
		{
			Type = ADEventType.FreeRoam;
		}
		public override ADEventType Type { get; }
		public float Duration { get; set; }
		public int Size { get; set; }
		public int PositionOffset { get; set; }
		public int OutTime { get; set; }
		[JsonProperty("OutEase")]
		public Ease.EaseType Ease { get; set; }
		public string HitsoundOnBeats { get; set; }
		public string HitsoundOffBeats { get; set; }
		public int CountdownTicks { get; set; }
		public int AngleCorrectionDir { get; set; }
	}
}
