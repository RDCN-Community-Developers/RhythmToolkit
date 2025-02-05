using System;
using RhythmBase.Components;
using RhythmBase.Events;
namespace RhythmBase.Adofai.Events
{
	public class ADSetFilter : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADSetFilter()
		{
			Type = ADEventType.SetFilter;
		}

		public override ADEventType Type { get; }

		public Filters Filter { get; set; }

		public string Enabled { get; set; }

		public int Intensity { get; set; }

		public float Duration { get; set; }

		public Ease.EaseType Ease { get; set; }

		public string DisableOthers { get; set; }

		public enum Filters
		{
			Grayscale,
			Sepia,
			Invert,
			VHS,
			EightiesTV,
			FiftiesTV,
			Arcade,
			LED,
			Rain,
			Blizzard,
			PixelSnow,
			Compression,
			Glitch,
			Pixelate,
			Waves,
			Static,
			Grain,
			MotionBlur,
			Fisheye,
			Aberration,
			Drawing,
			Neon,
			Handheld,
			NightVision,
			Funk,
			Tunnel,
			Weird3D,
			Blur,
			BlurFocus,
			GaussianBlur,
			HexagonBlack,
			Posterize,
			Sharpen,
			Contrast,
			EdgeBlackLine,
			OilPaint,
			SuperDot,
			WaterDrop,
			LightWater,
			Petals,
			PetalsInstant
		}
	}
}
