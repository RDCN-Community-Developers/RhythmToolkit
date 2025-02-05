using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Adofai.Events
{
	public class ADPositionTrack : ADBaseTileEvent
	{
		public ADPositionTrack()
		{
			Type = ADEventType.PositionTrack;
		}

		public override ADEventType Type { get; }

		public RDPoint PositionOffset { get; set; }

		public object RelativeTo
		{
			[CompilerGenerated]
			get
			{
				return RelativeTo;
			}
			[CompilerGenerated]
			set
			{
				RelativeTo = RuntimeHelpers.GetObjectValue(value);
			}
		}

		public float Rotation { get; set; }

		public float Scale { get; set; }

		public float Opacity { get; set; }

		public bool JustThisTile { get; set; }

		public bool SditorOnly { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool? StickToFloors { get; set; }
	}
}
