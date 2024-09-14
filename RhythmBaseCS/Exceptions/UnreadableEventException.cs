using System;
using RhythmBase.Events;

namespace RhythmBase.Exceptions
{

	public class UnreadableEventException(string message, BaseEvent item) : RhythmBaseException(message)
	{
		public BaseEvent Item = item;
	}
}
