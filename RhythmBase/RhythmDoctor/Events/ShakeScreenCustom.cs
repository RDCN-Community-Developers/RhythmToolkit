using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a custom screen shake event in Rhythm Doctor.
/// </summary>
public class ShakeScreenCustom : BaseEvent, IRoomEvent, IDurationEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.ShakeScreenCustom;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);

	/// <summary>
	/// Gets or sets the type of shake effect.
	/// </summary>
	public ShakeType ShakeType { get; set; } = ShakeType.Normal;
	///<inheritdoc/>
	public float Duration { get; set; } = 0.5f;

	/// <summary>
	/// Gets or sets the amplitude of the shake effect.
	/// </summary>
	public float Amplitude { get; set; } = 1f;

	/// <summary>
	/// Gets or sets a value indicating whether the duration is measured in beats.
	/// </summary>
	public bool UseBeats { get; set; } = false;

	/// <summary>
	/// Gets or sets the frequency of the shake effect.
	/// Only used when <see cref="ShakeType"/> is not <see cref="ShakeType.Normal"/>.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(ShakeType)} is not RhythmBase.RhythmDoctor.Events.{nameof(ShakeType)}.{nameof(ShakeType.Normal)}")]
	public float Frequency { get; set; } = 10f;

	/// <summary>
	/// Gets or sets a value indicating whether the shake effect should fade out.
	/// Only used when <see cref="ShakeType"/> is <see cref="ShakeType.Smooth"/> or <see cref="ShakeType.Rotate"/>.
	/// </summary>
	[RDJsonCondition($"""
    $&.{nameof(ShakeType)} is 
    RhythmBase.RhythmDoctor.Events.{nameof(ShakeType)}.{nameof(ShakeType.Smooth)} or
    RhythmBase.RhythmDoctor.Events.{nameof(ShakeType)}.{nameof(ShakeType.Rotate)}
    """)]
	public bool FadeOut { get; set; } = false;
}
