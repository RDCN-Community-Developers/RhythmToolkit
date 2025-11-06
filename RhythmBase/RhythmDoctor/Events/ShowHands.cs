using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Defines the possible actions for the event.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum ShowHandsActions
	{
		/// <summary>
		/// Show the hands.
		/// </summary>
		Show,
		/// <summary>
		/// Hide the hands.
		/// </summary>
		Hide,
		/// <summary>
		/// Raise the hands.
		/// </summary>
		Raise,
		/// <summary>
		/// Lower the hands.
		/// </summary>
		Lower
	}
	/// <summary>
	/// Represents an event to show hands in a room.
	/// </summary>
	public class ShowHands : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShowHands"/> class.
		/// </summary>
		public ShowHands() { }
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the action to be performed.
		/// </summary>
		public ShowHandsActions Action { get; set; } = ShowHandsActions.Show;
		/// <summary>
		/// Gets or sets the hand of the player.
		/// </summary>
		public PlayerHands Hand { get; set; } = PlayerHands.Right;
		/// <summary>
		/// Gets or sets a value indicating whether the hands should be aligned.
		/// </summary>
		public bool Align { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether the action should be instant.
		/// </summary>
		public bool ForceRaise { get; set; } = true;
		/// <summary>
		/// Gets or sets a value indicating whether the operation should be executed immediately.
		/// </summary>
		public bool Instant { get; set; } = true;
		/// <summary>
		/// Gets or sets the extent of the action.
		/// </summary>
		public ShowHandsExtents Extent { get; set; } = ShowHandsExtents.Full;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.ShowHands;

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;
	}
	/// <summary>
	/// Defines the possible extents for the action.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum ShowHandsExtents
	{
		/// <summary>
		/// Full extent.
		/// </summary>
		Full,
		/// <summary>
		/// Short extent.
		/// </summary>
		Short
	}
}
