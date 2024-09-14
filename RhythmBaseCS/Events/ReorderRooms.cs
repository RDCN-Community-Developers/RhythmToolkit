using System;
using System.Collections.Generic;

namespace RhythmBase.Events
{

	public class ReorderRooms : BaseEvent
	{

		public ReorderRooms()
		{
			Type = EventType.ReorderRooms;
			Tab = Tabs.Rooms;
		}


		public List<uint> Order { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }
	}
}
