using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a stutter event in a room.
/// </summary>
public record class Stutter : BaseEvent, IRoomEvent
{
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the source beat of the stutter event.
	/// </summary>
	public float SourceBeat { get; set; }
	/// <summary>
	/// Gets or sets the length of the stutter event.
	/// </summary>
	public float Length { get; set; } = 1f;
	/// <summary>
	/// Gets or sets the action of the stutter event.
	/// </summary>
	public StutterAction Action { get; set; } = StutterAction.Add;
	/// <summary>
	/// Gets or sets the number of loops for the stutter event.
	/// </summary>
	public int Loops { get; set; } = 1;
	///<inheritdoc/>
	public override EventType Type => EventType.Stutter;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}