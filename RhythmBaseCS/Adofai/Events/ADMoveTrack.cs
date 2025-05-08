using System;
using System.Runtime.CompilerServices;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Adofai.Events
{
	public class ADMoveTrack : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADMoveTrack()
		{
			Type = ADEventType.MoveTrack;
		}

		public override ADEventType Type { get; }

		public object StartTile
		{
			[CompilerGenerated]
			get
			{
				return StartTile;
			}
			[CompilerGenerated]
			set
			{
				StartTile = RuntimeHelpers.GetObjectValue(value);
			}
		}

		public object EndTile
		{
			[CompilerGenerated]
			get
			{
				return EndTile;
			}
			[CompilerGenerated]
			set
			{
				EndTile = RuntimeHelpers.GetObjectValue(value);
			}
		}

		public int GapLength { get; set; }

		public float Duration { get; set; }

		public RDPoint PositionOffset { get; set; }

		public EaseType Ease { get; set; }

		public bool MaxVfxOnly { get; set; }
	}
}
