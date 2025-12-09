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
/// <summary>
/// Specifies the possible durations for a flash event.
/// </summary>
[RDJsonEnumSerializable]
public enum DurationType
{
	/// <summary>
	/// A short duration.
	/// </summary>
	Short = 1,
	/// <summary>
	/// A medium duration.
	/// </summary>
	Medium = 2,
	/// <summary>
	/// A long duration.
	/// </summary>
	Long = 4,
}
