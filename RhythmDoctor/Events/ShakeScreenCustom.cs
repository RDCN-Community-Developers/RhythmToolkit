using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	public class ShakeScreenCustom : BaseEvent, IRoomEvent, IDurationEvent
	{
		public override EventType Type => EventType.ShakeScreenCustom;
		public override Tabs Tab => Tabs.Actions;
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		public ShakeType ShakeType { get; set; } = ShakeType.Normal;
		public float Duration { get; set; } = 0.5f;
		public float Amplitude { get; set; } = 1f;
		public bool UseBeats { get; set; } = false;
		[RDJsonCondition($"$&.{nameof(ShakeType)} is not RhythmBase.RhythmDoctor.Events.{nameof(ShakeType)}.{nameof(ShakeType.Normal)}")]
		public float Frequency { get; set; } = 10f;
		[RDJsonCondition($"""
			$&.{nameof(ShakeType)} is 
			RhythmBase.RhythmDoctor.Events.{nameof(ShakeType)}.{nameof(ShakeType.Smooth)} or
			RhythmBase.RhythmDoctor.Events.{nameof(ShakeType)}.{nameof(ShakeType.Rotate)}
			""")]
		public bool FadeOut { get; set; } = false;
	}
}
