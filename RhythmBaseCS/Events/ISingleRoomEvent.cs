using System;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public interface ISingleRoomEvent : IBaseEvent
	{
		SingleRoom Room { get; set; }
	}
}
