using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a Flash event in the rhythm base.
/// </summary>
public class Flash : BaseEvent
{
	/// <summary>
	/// Gets or sets the rooms associated with the flash event.
	/// </summary>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the duration of the flash event.
	/// </summary>
	public DurationType Duration { get; set; } = DurationType.Short;
	///<inheritdoc/>
	public override EventType Type => EventType.Flash;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {Duration}";
}