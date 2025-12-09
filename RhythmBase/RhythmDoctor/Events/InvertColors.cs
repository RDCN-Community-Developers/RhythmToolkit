using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that inverts colors in a room.
/// </summary>
public class InvertColors : BaseEvent, IRoomEvent
{
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets a value indicating whether the color inversion is enabled.
	/// </summary>
	public bool Enable { get; set; } = true;
	///<inheritdoc/>
	public override EventType Type => EventType.InvertColors;

	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;

	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {Enable}";
}
