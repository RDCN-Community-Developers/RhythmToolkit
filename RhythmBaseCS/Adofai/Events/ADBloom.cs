using System;
using RhythmBase.Components;
using RhythmBase.Events;
using SkiaSharp;
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

		public SKColor Color { get; set; }

		public float Duration { get; set; }

		public Ease.EaseType Ease { get; set; }
	}
}
