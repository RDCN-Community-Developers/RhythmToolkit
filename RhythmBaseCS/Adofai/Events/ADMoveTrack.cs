using System;
using System.Runtime.CompilerServices;
using RhythmBase.Components;
using RhythmBase.Events;

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
				return this.StartTile;
			}
			[CompilerGenerated]
			set
			{
				this.StartTile = RuntimeHelpers.GetObjectValue(value);
			}
		}


		public object EndTile
		{
			[CompilerGenerated]
			get
			{
				return this.EndTile;
			}
			[CompilerGenerated]
			set
			{
				this.EndTile = RuntimeHelpers.GetObjectValue(value);
			}
		}


		public int GapLength { get; set; }


		public float Duration { get; set; }


		public RDPoint PositionOffset { get; set; }


		public Ease.EaseType Ease { get; set; }


		public bool MaxVfxOnly { get; set; }
	}
}
