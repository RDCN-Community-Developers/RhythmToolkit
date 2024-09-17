using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class CallCustomMethod : BaseEvent
	{
		public CallCustomMethod()
		{
			Type = EventType.CallCustomMethod;
			Rooms = Room.Default();
			Tab = Tabs.Actions;
		}

		public string MethodName { get; set; }

		public ExecutionTimeOptions ExecutionTime { get; set; }

		public int SortOffset { get; set; }

		public override EventType Type { get; }

		[JsonIgnore]
		public Room Rooms { get; set; }

		public override Tabs Tab { get; }

		public override string ToString() => base.ToString() + string.Format(" {0}", MethodName);

		public enum ExecutionTimeOptions
		{
			OnPrebar,
			OnBar
		}
	}
}
