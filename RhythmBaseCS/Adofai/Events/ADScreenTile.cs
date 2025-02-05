using System;
using RhythmBase.Components;
using RhythmBase.Events;
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

		public Ease.EaseType Ease { get; set; }
	}
}
