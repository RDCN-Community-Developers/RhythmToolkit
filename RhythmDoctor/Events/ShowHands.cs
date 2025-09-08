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
		public ShowHands()
		{
			Rooms = new RDRoom([0]);
			Type = EventType.ShowHands;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }
		/// <summary>
		/// Gets or sets the action to be performed.
		/// </summary>
		public ShowHandsActions Action { get; set; }
		/// <summary>
		/// Gets or sets the hand of the player.
		/// </summary>
		public PlayerHands Hand { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the hands should be aligned.
		/// </summary>
		public bool Align { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the action should be instant.
		/// </summary>
		public bool ForceRaise { get; set; }
		public bool Instant { get; set; }
		/// <summary>
		/// Gets or sets the extent of the action.
		/// </summary>
		public ShowHandsExtents Extent { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
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
