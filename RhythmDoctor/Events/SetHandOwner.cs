using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the owner of a hand in a room.
	/// </summary>
	public class SetHandOwner : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetHandOwner"/> class.
		/// </summary>
		public SetHandOwner()
		{
			Rooms = new RDRoom([0]);
			Type = EventType.SetHandOwner;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the room associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }
		/// <summary>
		/// Gets or sets the hand associated with the event.
		/// </summary>
		public PlayerHands Hand { get; set; }
		/// <summary>
		/// Gets or sets the character associated with the event.
		/// </summary>
		public RDCharacters Character { get; set; }
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
