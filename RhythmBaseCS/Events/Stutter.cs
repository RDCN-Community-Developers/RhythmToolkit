using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents a stutter event in a room.
	/// </summary>
	public class Stutter : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Stutter"/> class.
		/// </summary>
		public Stutter()
		{
			Rooms = new Room(false, new byte[1]);
			Type = EventType.Stutter;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the room associated with the stutter event.
		/// </summary>
		public Room Rooms { get; set; }

		/// <summary>
		/// Gets or sets the source beat of the stutter event.
		/// </summary>
		public float SourceBeat { get; set; }

		/// <summary>
		/// Gets or sets the length of the stutter event.
		/// </summary>
		public float Length { get; set; }

		/// <summary>
		/// Gets or sets the action of the stutter event.
		/// </summary>
		public Actions Action { get; set; }

		/// <summary>
		/// Gets or sets the number of loops for the stutter event.
		/// </summary>
		public int Loops { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Defines the possible actions for the stutter event.
		/// </summary>
		public enum Actions
		{
			/// <summary>
			/// Add action.
			/// </summary>
			Add,

			/// <summary>
			/// Cancel action.
			/// </summary>
			Cancel
		}
	}
}
