using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a camera pulse event in a room.
/// </summary>
public record class PulseCamera : BaseEvent, IRoomEvent
{
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the strength of the pulse.
	/// </summary>
	public int Strength { get; set; } = 1;
	/// <summary>
	/// Gets or sets the count of pulses.
	/// </summary>
	public int Count { get; set; } = 1;
	/// <summary>
	/// Gets or sets the frequency of the pulses.
	/// </summary>
	public float Frequency { get; set; } = 1;
	///<inheritdoc/>
	public override EventType Type => EventType.PulseCamera;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}
