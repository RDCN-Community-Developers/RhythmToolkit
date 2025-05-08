using System;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Adofai.Events
{
	public class ADScreenTile : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADScreenTile()
		{
			Type = ADEventType.ScreenTile;
		}

		public override ADEventType Type { get; }

		public float Duration { get; set; }

		public RDPoint Tile { get; set; }

		public EaseType Ease { get; set; }
	}
}
