using System;
using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class FadeRoom : BaseEvent, IEaseEvent
	{

		public FadeRoom()
		{
			Type = EventType.FadeRoom;
			Tab = Tabs.Rooms;
		}


		public Ease.EaseType Ease { get; set; }


		[EaseProperty]
		public uint Opacity { get; set; }


		public float Duration { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		[JsonIgnore]
		public Room Room
		{
			get
			{
				return new SingleRoom(checked((byte)Y));
			}
		}
	}
}
