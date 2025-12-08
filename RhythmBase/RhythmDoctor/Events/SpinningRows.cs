using RhythmBase.Global.Components.Easing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Events;
/// <summary>
/// Represents an event that applies a spinning rows animation with configurable easing and duration.[Not used]
/// </summary>
public class SpinningRows : BaseRowAnimation, IEaseEvent
{
	public string Text { get; set; } = "";
	public SpiningAction Action { get; set; }
	[RDJsonCondition($"$&.{nameof(Action)} is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Connect)}")]
	public int ToRow { get; set; }
	[RDJsonCondition($"""
		$&.{nameof(Action)}
		is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Rotate)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.ConstantRotation)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Split)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}
		""")]
	public float Angle { get; set; }
	[RDJsonCondition($"""
		$&.{nameof(Amplitude)} is not null &&
		$&.{nameof(Action)}
		is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
		""")]
	public float? Amplitude { get; set; }
	[RDJsonCondition($"""
		$&.{nameof(Amplitude)} is not null &&
		$&.{nameof(Action)}
		is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
		""")]
	public float? Frequency { get; set; }
	[RDJsonCondition($"$&.{nameof(Action)} is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}")]
	public bool DoEffects { get; set; }
	[RDJsonCondition($"""
		$&.{nameof(Action)}
		is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Rotate)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.ConstantRotation)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Split)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
		""")]
	public EaseType Ease { get; set; }
	[RDJsonCondition($"""
		$&.{nameof(Action)}
		is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Rotate)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.ConstantRotation)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Split)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}
		or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
		""")]
	public float Duration { get; set; }
	public override EventType Type => EventType.SpinningRows;
	public override Tabs Tab => Tabs.Windows;
}
[RDJsonEnumSerializable]
public enum SpiningAction
{
	Connect,
	Disconnect,
	Rotate,
	ConstantRotation,
	WavyRotation,
	Merge,
	Split,
}