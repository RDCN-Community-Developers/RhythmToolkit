using RhythmBase.Components;

namespace RhythmBase.Events
{
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
			Tab = Tabs.Rooms;
		}

		/// <summary>
		/// Gets or sets the order of the rooms.
		/// </summary>
		public RoomOrder Order { get; set; } = new();

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
}
