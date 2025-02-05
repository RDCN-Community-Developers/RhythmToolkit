using System;
using SkiaSharp;
namespace RhythmBase.Adofai.Events
{
	public class ADColorTrack : ADBaseTileEvent
	{
		public ADColorTrack()
		{
			Type = ADEventType.ColorTrack;
		}

		public override ADEventType Type { get; }

		public ADTrackColorTypes TrackColorType { get; set; }

		public SKColor TrackColor { get; set; }

		public SKColor SecondaryTrackColor { get; set; }

		public float TrackColorAnimDuration { get; set; }

		public ADTrackColorPulses TrackColorPulse { get; set; }

		public float TrackPulseLength { get; set; }

		public ADTrackStyles TrackStyle { get; set; }

		public string TrackTexture { get; set; }

		public float TrackTextureScale { get; set; }

		public float TrackGlowIntensity { get; set; }
	}
}
