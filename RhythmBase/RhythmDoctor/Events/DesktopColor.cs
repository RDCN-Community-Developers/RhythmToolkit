using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	public class DesktopColor : BaseWindowEvent, IEaseEvent
	{
		public override int Y => 0;
		[RDJsonCondition($"$&.{nameof(StartColor)} is not null")]
		public PaletteColor? StartColor { get; set; }
		[RDJsonCondition($"$&.{nameof(EndColor)} is not null")]
		public PaletteColor? EndColor { get; set; }
		public override EventType Type => EventType.DesktopColor;
		public EaseType Ease { get; set; }
		public float Duration { get; set; }
	}
}
