using System;
using RhythmBase.Events;
namespace RhythmBase.Exceptions
{
	public class UnreadableEventException(string message, IBaseEvent item) : RhythmBaseException(message)
	{
		public IBaseEvent Item = item;
	}
}
