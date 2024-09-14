using System;
using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class WindowResize : BaseEvent, IEaseEvent
	{

		public WindowResize()
		{
			Type = EventType.WindowResize;
			Tab = Tabs.Actions;
		}


		[JsonIgnore]
		public SingleRoom Room
		{
			get
			{
				SingleRoom Room = new(checked((byte)Y));
				return Room;
			}
		}


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		[EaseProperty]
		public PointE? Scale { get; set; }


		[EaseProperty]
		public PointE? Pivot { get; set; }


		public float Duration { get; set; }


		public Ease.EaseType Ease { get; set; }
	}
}
