using Newtonsoft.Json;
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
		public RoomContentModes Mode { get; set; }
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
		public RDRoom Room
		{
			get
			{
				return new RDSingleRoom(checked((byte)Y));
			}
		}
	}
	/// <summary>
	/// Defines the modes for room content.
	/// </summary>
	public enum RoomContentModes
	{
#pragma warning disable CS1591
		Center,
		ScaleToFill,
		AspectFit,
		AspectFill,
		Tiled,
		Real
#pragma warning restore CS1591
	}
}
