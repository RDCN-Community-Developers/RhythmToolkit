using System;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	public class ADBloom : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADBloom()
		{
			Type = ADEventType.Bloom;
		}

		public override ADEventType Type { get; }

		public bool Enabled { get; set; }

		public int Threshold { get; set; }

		public int Intensity { get; set; }

		public RDColor Color { get; set; }

		public float Duration { get; set; }

		public EaseType Ease { get; set; }
	}
}
