using System;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public interface ISingleRoomEvent
	{

		SingleRoom Room { get; set; }
	}
}
