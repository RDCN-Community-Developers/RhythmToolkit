using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class SetSpeed : BaseEvent, IEaseEvent
	{
		public SetSpeed()
		{
			Type = EventType.SetSpeed;
			Rooms = Room.Default();
			Tab = Tabs.Actions;
		}

		public Ease.EaseType Ease { get; set; }

		[EaseProperty]
		public float Speed { get; set; }

		public float Duration { get; set; }

		public override EventType Type { get; }

		[JsonIgnore]
		public Room Rooms { get; set; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" Speed:{0}", Speed);
	}
}
