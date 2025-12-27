namespace RhythmBase.RhythmDoctor.Events;

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
	[RDJsonCondition($"$&.{nameof(Action)} is RhythmBase.RhythmDoctor.Events.{nameof(PulseAction)}.{nameof(PulseAction.Custom)}")]
	public uint CustomPulse { get; set; } = 0;
	/// <summary>
	/// Gets or sets the action type.
	/// </summary>
	public PulseAction Action { get; set; } = PulseAction.Increment;
	///<inheritdoc/>
	public override EventType Type => EventType.PulseFreeTimeBeat;
	///<inheritdoc/>
	public override string ToString()
	{
		string Out = "";
		switch (Action)
		{
			case PulseAction.Increment:
				Out = ">";
				break;
			case PulseAction.Decrement:
				Out = "<";
				break;
			case PulseAction.Custom:
				Out = (CustomPulse + 1).ToString();
				break;
			case PulseAction.Remove:
				Out = "X";
				break;
		}
		return base.ToString() + $"{Out}";
	}
}
