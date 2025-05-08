using System;
using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Adofai.Events
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ADMoveDecorations : ADBaseTaggedTileAction, IEaseEvent
	{
		public ADMoveDecorations()
		{
			Type = ADEventType.MoveDecorations;
		}

		public override ADEventType Type { get; }

		public float Duration { get; set; }

		public string Tag { get; set; }

		public EaseType Ease { get; set; }

		public RDPoint? PositionOffset { get; set; }

		public RDPoint? ParallaxOffset { get; set; }

		public bool? Visible { get; set; }

		public ADDecorationRelativeTo? RelativeTo { get; set; }

		public string DecorationImage { get; set; }

		public RDSize? PivotOffset { get; set; }

		public float? RotationOffset { get; set; }

		public RDSize? Scale { get; set; }

		public RDColor? Color { get; set; }

		public float? Opacity { get; set; }

		public int? Depth { get; set; }

		public RDPoint? Parallax { get; set; }

		public MaskingTypes? MaskingType { get; set; }

		public bool? UseMaskingDepth { get; set; }

		public int? MaskingFrontDepth { get; set; }

		public int? MaskingBackDepth { get; set; }

		public enum MaskingTypes
		{
			None,
			Mask,
			VisibleInsideMask,
			VisibleOutsideMask
		}
	}
}
