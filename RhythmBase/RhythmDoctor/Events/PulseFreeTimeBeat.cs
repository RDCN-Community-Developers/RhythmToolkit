namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Defines the action types for the pulse free time beat.
/// </summary>
[RDJsonEnumSerializable]
public enum PulseActions
{
	/// <summary>
	/// Increment action.
	/// </summary>
	Increment,
	/// <summary>
	/// Decrement action.
	/// </summary>
	Decrement,
	/// <summary>
	/// Custom action.
	/// </summary>
	Custom,
	/// <summary>
	/// Remove action.
	/// </summary>
	Remove
}
/// <summary>
/// Represents a pulse free time beat event.
/// </summary>
public class PulseFreeTimeBeat : BaseBeat
{
	/// <summary>
	/// Gets or sets the hold duration.
	/// </summary>
	public float Hold { get; set; } = 0;
	/// <summary>
	/// Gets or sets the custom pulse value.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Action)} is RhythmBase.RhythmDoctor.Events.{nameof(PulseActions)}.{nameof(PulseActions.Custom)}")]
	public uint CustomPulse { get; set; } = 0;
	/// <summary>
	/// Gets or sets the action type.
	/// </summary>
	public PulseActions Action { get; set; } = PulseActions.Increment;
	///<inheritdoc/>
	public override EventType Type => EventType.PulseFreeTimeBeat;
	///<inheritdoc/>
	public override string ToString()
	{
		string Out = "";
		switch (Action)
		{
			case PulseActions.Increment:
				Out = ">";
				break;
			case PulseActions.Decrement:
				Out = "<";
				break;
			case PulseActions.Custom:
				Out = (CustomPulse + 1).ToString();
				break;
			case PulseActions.Remove:
				Out = "X";
				break;
		}
		return base.ToString() + $"{Out}";
	}
}
