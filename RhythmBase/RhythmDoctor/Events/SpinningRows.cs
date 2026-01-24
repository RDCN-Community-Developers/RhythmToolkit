using RhythmBase.Global.Components.Easing;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that applies a spinning rows animation with configurable easing and duration.
/// </summary>
public record class SpinningRows : BaseRowAnimation, IEaseEvent
{
	/// <summary>
	/// The text to display during the spinning animation (if applicable).
	/// </summary>
	public string Text { get; set; } = "";

	/// <summary>
	/// Specifies the type of spinning action to perform.
	/// </summary>
	public SpiningAction Action { get; set; }

	/// <summary>
	/// Target row index to connect to when <see cref="Action"/> is <see cref="SpiningAction.Connect"/>.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Action)} is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Connect)}")]
	public int ToRow { get; set; }

	/// <summary>
	/// Rotation angle in degrees used by rotation-related actions.
	/// </summary>
	[RDJsonCondition($"""
			$&.{nameof(Action)}
			is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Rotate)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.ConstantRotation)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Split)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}
			""")]
	public float Angle { get; set; }

	/// <summary>
	/// Amplitude for wavy rotation effects.
	/// </summary>
	[RDJsonCondition($"""
			$&.{nameof(Amplitude)} is not null &&
			$&.{nameof(Action)}
			is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
			""")]
	public float? Amplitude { get; set; }

	/// <summary>
	/// Frequency for wavy rotation effects.
	/// </summary>
	[RDJsonCondition($"""
			$&.{nameof(Amplitude)} is not null &&
			$&.{nameof(Action)}
			is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
			""")]
	public float? Frequency { get; set; }

	/// <summary>
	/// When true, additional visual effects are applied during merge operations.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Action)} is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}")]
	public bool DoEffects { get; set; }

	///<inheritdoc/>
	[RDJsonCondition($"""
			$&.{nameof(Action)}
			is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Rotate)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.ConstantRotation)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Split)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
			""")]
	public EaseType Ease { get; set; }

	///<inheritdoc/>
	[RDJsonCondition($"""
			$&.{nameof(Action)}
			is RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Rotate)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.ConstantRotation)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Split)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.Merge)}
			or RhythmBase.RhythmDoctor.Events.{nameof(SpiningAction)}.{nameof(SpiningAction.WavyRotation)}
			""")]
	public float Duration { get; set; }
	///<inheritdoc/>
	public override EventType Type => EventType.SpinningRows;

	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}