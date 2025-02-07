using System;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
using RhythmBase.Events;
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

		public RDColor StartColor { get; set; }

		public float StartOpacity { get; set; }

		public RDColor EndColor { get; set; }

		public float EndOpacity { get; set; }

		public EaseType Ease { get; set; }
	}
}
