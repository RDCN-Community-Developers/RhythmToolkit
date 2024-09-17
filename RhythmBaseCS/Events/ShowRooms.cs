using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{
	public class ShowRooms : BaseEvent, IEaseEvent, IRoomEvent
	{
		public ShowRooms()
		{
			Rooms = new Room(false, new byte[1]);
			Heights = new List<int>(4);
			Type = EventType.ShowRooms;
			Tab = Tabs.Rooms;
		}
		[JsonProperty]
		public Room Rooms { get; set; }
		public Ease.EaseType Ease { get; set; }
		public List<int> Heights { get; set; }
		[JsonProperty("transitionTime")]
		public float Duration { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
	}
}
