using System;
using RhythmBase.Components;
using RhythmBase.Events;

namespace RhythmBase.Adofai.Events
{

	public class ADShakeScreen : ADBaseTaggedTileAction, IEaseEvent
	{

		public ADShakeScreen()
		{
			Type = ADEventType.ShakeScreen;
		}


		public override ADEventType Type { get; }


		public float Duration { get; set; }


		public float Strength { get; set; }


		public float Intensity { get; set; }


		public Ease.EaseType Ease { get; set; }


		public float FadeOut { get; set; }
	}
}
