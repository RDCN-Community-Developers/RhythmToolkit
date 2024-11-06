using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event that flips the screen in a room.
	/// </summary>
	public class FlipScreen : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FlipScreen"/> class.
		/// </summary>
		public FlipScreen()
		{
			Rooms = new Room(true, new byte[1]);
			Type = EventType.FlipScreen;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the rooms associated with this event.
		/// </summary>
		public Room Rooms { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the screen should be flipped horizontally.
		/// </summary>
		public bool FlipX { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the screen should be flipped vertically.
		/// </summary>
		public bool FlipY { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab where this event is categorized.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			string result =
				FlipX
				? FlipY
					? "X"
					: "^v"
				: FlipY
					? "<>"
					: "";
			return base.ToString() + string.Format(" {0}", result);
		}
	}
}
