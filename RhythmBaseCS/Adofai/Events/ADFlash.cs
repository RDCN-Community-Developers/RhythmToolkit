using System;
using RhythmBase.Components;
using RhythmBase.Events;
using SkiaSharp;
namespace RhythmBase.Adofai.Events
{
	public class ADFlash : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADFlash()
		{
			Type = ADEventType.Flash;
		}

		public override ADEventType Type { get; }

		public float Duration { get; set; }

		public string Plane { get; set; }

		public SKColor StartColor { get; set; }

		public float StartOpacity { get; set; }

		public SKColor EndColor { get; set; }

		public float EndOpacity { get; set; }

		public Ease.EaseType Ease { get; set; }
	}
}
