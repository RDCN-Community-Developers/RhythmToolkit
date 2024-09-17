using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public interface IRoomEvent : IBaseEvent
	{
		Room Rooms { get; set; }
	}
}
