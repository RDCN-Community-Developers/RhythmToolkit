using System;
using RhythmBase.Global.Components;
namespace RhythmBase.Adofai.Events
{
	public class ADCustomBackground : ADBaseTaggedTileAction
	{
		public ADCustomBackground()
		{
			Type = ADEventType.CustomBackground;
		}

		public override ADEventType Type { get; }

		public RDColor Color { get; set; }

		public string BgImage { get; set; }

		public RDColor ImageColor { get; set; }

		public RDPoint Parallax { get; set; }

		public BgDisplayModes BgDisplayMode { get; set; }

		public bool ImageSmoothing { get; set; }

		public bool LockRot { get; set; }

		public bool LoopBG { get; set; }

		public float ScalingRatio { get; set; }

		public enum BgDisplayModes
		{
			FitToScreen,
			Unscaled,
			Tiled
		}
	}
}
