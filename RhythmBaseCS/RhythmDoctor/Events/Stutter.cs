using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a stutter event in a room.
	/// </summary>
	public class Stutter : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Stutter"/> class.
		/// </summary>
		public Stutter() { }
		/// <summary>
		/// Gets or sets the room associated with the stutter event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the source beat of the stutter event.
		/// </summary>
		public float SourceBeat
		{
			get => field;
			set => field = value;
		}
		/// <summary>
		/// Gets or sets the length of the stutter event.
		/// </summary>
		public float Length { get; set; } = 1f;
		/// <summary>
		/// Gets or sets the action of the stutter event.
		/// </summary>
		public StutterActions Action { get; set; } = StutterActions.Add;
		/// <summary>
		/// Gets or sets the number of loops for the stutter event.
		/// </summary>
		public int Loops { get; set; } = 1;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.Stutter;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
	}
	/// <summary>
	/// Defines the possible actions for the stutter event.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum StutterActions
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
