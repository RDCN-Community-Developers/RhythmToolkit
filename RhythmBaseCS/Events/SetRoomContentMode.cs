using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
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
		public Modes Mode { get; set; }

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
		[JsonIgnore]
		public Room Room
		{
			get
			{
				return new SingleRoom(checked((byte)Y));
			}
		}

		/// <summary>
		/// Defines the modes for room content.
		/// </summary>
		public enum Modes
		{
			/// <summary>
			/// Center mode.
			/// </summary>
			Center,

			/// <summary>
			/// Aspect fill mode.
			/// </summary>
			AspectFill
		}
	}
}
