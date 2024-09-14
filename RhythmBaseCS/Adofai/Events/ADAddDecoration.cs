using System;
using RhythmBase.Components;
using SkiaSharp;

namespace RhythmBase.Adofai.Events
{

	public class ADAddDecoration : ADBaseTileEvent
	{

		public ADAddDecoration()
		{
			Type = ADEventType.AddDecoration;
		}


		public override ADEventType Type { get; }


		public string DecorationImage { get; set; }


		public RDPointN Position { get; set; }


		public ADDecorationRelativeTo RelativeTo { get; set; }


		public RDSizeN PivotOffset { get; set; }


		public float Rotation { get; set; }


		public bool LockRotation { get; set; }


		public RDSizeN Scale { get; set; }


		public bool LockScale { get; set; }


		public RDSizeN Tile { get; set; }


		public SKColor Color { get; set; }


		public float Opacity { get; set; }


		public int Depth { get; set; }


		public RDSizeN Parallax { get; set; }


		public RDSizeN ParallaxOffset { get; set; }


		public string Tag { get; set; }


		public bool ImageSmoothing { get; set; }


		public BlendModes BlendMode { get; set; }


		public MaskingTypes MaskingType { get; set; }


		public bool UseMaskingDepth { get; set; }


		public int MaskingFrontDepth { get; set; }


		public int MaskingBackDepth { get; set; }


		public HitboxTypes Hitbox { get; set; }


		public string HitboxEventTag { get; set; }


		public FailHitboxTypes FailHitboxType { get; set; }


		public RDSizeN FailHitboxScale { get; set; }


		public RDSizeN FailHitboxOffset { get; set; }


		public int FailHitboxRotation { get; set; }


		public string Components { get; set; }


		public enum BlendModes
		{

			None,

			Darken,

			Multiply,

			ColorBurn,

			LinearBurn,

			DarkerColor,

			Lighten,

			Screen,

			ColorDodge,

			LinearDodge,

			LighterColor,

			Overlay,

			SoftLight,

			HardLight,

			VividLight,

			LinearLight,

			PinLight,

			HardMix,

			Difference,

			Exclusion,

			Subtract,

			Divide,

			Hue,

			Saturation,

			Color,

			Luminosity
		}


		public enum MaskingTypes
		{

			None,

			Mask,

			VisibleInsideMask,

			VisibleOutsideMask
		}


		public enum HitboxTypes
		{

			None,

			Kill,

			Event
		}


		public enum FailHitboxTypes
		{

			Box,

			Circle,

			Capsule
		}
	}
}
