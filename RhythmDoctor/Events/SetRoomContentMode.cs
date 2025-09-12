using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the room content mode.
	/// </summary>
	public class SetRoomContentMode : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetRoomContentMode"/> class.
		/// </summary>
		public SetRoomContentMode()
		{
			Type = EventType.SetRoomContentMode;
			Tab = Tabs.Rooms;
		}
		/// <summary>
		/// Gets or sets the mode of the room content.
		/// </summary>
		public ContentModes Mode { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
		/// <summary>
		/// Gets the room associated with the event.
		/// </summary>
		[RDJsonIgnore]
		public RDRoom Room
		{
			get
			{
				return new RDSingleRoom(checked((byte)Y));
			}
		}
	}
}
