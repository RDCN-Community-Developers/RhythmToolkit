using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that calls a custom method.
/// </summary>
public partial record class CallCustomMethod : BaseEvent, IRoomEvent
{
	/// <summary>
	/// Gets or sets the name of the method to be called.
	/// </summary>
	public string MethodName { get; set; } = "";
	/// <summary>
	/// Gets or sets the execution time of the method.
	/// </summary>
	public EventExecutionTimeOption ExecutionTime { get; set; } = EventExecutionTimeOption.OnBar;
	/// <summary>
	/// Gets or sets the sort offset for the event.
	/// </summary>
	public int SortOffset { get; set; }
	/// <inheritdoc/>
	public override EventType Type => EventType.CallCustomMethod;
	/// <inheritdoc/>
	[RDJsonIgnore]
	public RDRoom Rooms { get; set; } = RDRoom.Default;
	/// <inheritdoc/>
	public override Tab Tab => Tab.Actions;
	/// <inheritdoc/>
	public override string ToString() => base.ToString() + $" {MethodName}";
}