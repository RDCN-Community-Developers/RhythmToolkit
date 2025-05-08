using System;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
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
