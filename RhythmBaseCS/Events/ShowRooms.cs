using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
namespace RhythmBase.Events
{
	/// <summary>  
	/// Represents an event to show rooms.  
	/// </summary>  
	public class ShowRooms : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="ShowRooms"/> class.  
		/// </summary>  
		public ShowRooms()
		{
			Rooms = new RDRoom(false, new byte[1]);
			Heights = new List<int>(4);
			Type = EventType.ShowRooms;
			Tab = Tabs.Rooms;
		}

		/// <summary>  
		/// Gets or sets the rooms associated with the event.  
		/// </summary>  
		[JsonProperty]
		public RDRoom Rooms { get; set; }

		/// <summary>  
		/// Gets or sets the ease type for the event.  
		/// </summary>  
		public EaseType Ease { get; set; }

		/// <summary>  
		/// Gets or sets the heights associated with the event.  
		/// </summary>
		public List<int> Heights { get; set; }

		/// <summary>  
		/// Gets or sets the duration of the transition.  
		/// </summary>  
		[JsonProperty("transitionTime")]
		public float Duration { get; set; }

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
