using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to reorder rooms.
/// </summary>
public class ReorderRooms : BaseEvent
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ReorderRooms"/> class.
	/// </summary>
	public ReorderRooms()
	{
		Type = EventType.ReorderRooms;
		Tab = Tab.Rooms;
	}
	/// <summary>
	/// Gets or sets the order of the rooms.
	/// </summary>
	public RoomOrder Order { get; set; } = new();
	///<inheritdoc/>
	public override EventType Type { get; }
	///<inheritdoc/>
	public override Tab Tab { get; }
}
