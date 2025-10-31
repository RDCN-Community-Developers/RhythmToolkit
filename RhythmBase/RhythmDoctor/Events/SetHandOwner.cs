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
		public SetHandOwner() { }
		/// <summary>
		/// Gets or sets the room associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the hand associated with the event.
		/// </summary>
		public PlayerHands Hand { get; set; } = PlayerHands.Right;
		/// <summary>
		/// Gets or sets the character associated with the event.
		/// </summary>
		public RDCharacters Character { get; set; } = RDCharacters.Player;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.SetHandOwner;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
	}
}
