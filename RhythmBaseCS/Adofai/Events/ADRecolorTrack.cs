using System;
using System.Runtime.CompilerServices;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
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

		public ADTrackColorTypes TrackColorType { get; set; }

		public RDColor TrackColor { get; set; }

		public RDColor SecondaryTrackColor { get; set; }

		public float TrackColorAnimDuration { get; set; }

		public ADTrackColorPulses TrackColorPulse { get; set; }

		public float TrackPulseLength { get; set; }

		public ADTrackStyles TrackStyle { get; set; }

		public float TrackGlowIntensity { get; set; }

		public EaseType Ease { get; set; }
	}
}
