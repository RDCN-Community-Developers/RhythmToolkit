using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to set the owner of a hand in a room.
/// </summary>
public class SetHandOwner : BaseEvent, IRoomEvent
{
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the hand associated with the event.
	/// </summary>
	public PlayerHand Hand { get; set; } = PlayerHand.Right;
	/// <summary>
	/// Gets or sets the character associated with the event.
	/// </summary>
	public RDCharacters Character { get; set; } = RDCharacters.Player;
	///<inheritdoc/>
	public override EventType Type => EventType.SetHandOwner;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;
}
