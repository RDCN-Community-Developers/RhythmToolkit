using Newtonsoft.Json;
using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Events;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to show rooms.  
	/// </summary>  
	public class ShowRooms : BaseEvent, IEaseEvent, IRoomEvent
	{
		private RoomHeight _height = new();
		/// <summary>  
		/// Initializes a new instance of the <see cref="ShowRooms"/> class.  
		/// </summary>  
		public ShowRooms() { }
		/// <summary>  
		/// Gets or sets the height configuration for the room.  
		/// </summary>  
		[JsonIgnore]
		public RoomHeight Height { get => _height; set => _height = value; }
		/// <summary>  
		/// Gets or sets the room configuration associated with the height.  
		/// </summary>  
		[JsonIgnore]
		public RDRoom Rooms
		{
			get => _height.Room;
			set
			{
				_height.Room = value;
			}
		}
		/// <summary>  
		/// Gets or sets the ease type for the event.  
		/// </summary>  
		public EaseType Ease { get; set; }
		/// <summary>  
		/// Gets or sets the duration of the transition.  
		/// </summary>  
		[JsonProperty("transitionTime")]
		public float Duration { get; set; }
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type { get; } = EventType.ShowRooms;

		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab { get; } = Tabs.Rooms;
	}
}
