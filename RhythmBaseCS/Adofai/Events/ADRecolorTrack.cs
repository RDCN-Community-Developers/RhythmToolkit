using System;
using System.Runtime.CompilerServices;
using RhythmBase.Components;
using RhythmBase.Events;
using SkiaSharp;

namespace RhythmBase.Adofai.Events
{

	public class ADRecolorTrack : ADBaseTaggedTileAction, IEaseEvent
	{

		public ADRecolorTrack()
		{
			Type = ADEventType.RecolorTrack;
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


		public ADTrackColorTypes TrackColorType { get; set; }


		public SKColor TrackColor { get; set; }


		public SKColor SecondaryTrackColor { get; set; }


		public float TrackColorAnimDuration { get; set; }


		public ADTrackColorPulses TrackColorPulse { get; set; }


		public float TrackPulseLength { get; set; }


		public ADTrackStyles TrackStyle { get; set; }


		public float TrackGlowIntensity { get; set; }


		public Ease.EaseType Ease { get; set; }
	}
}
