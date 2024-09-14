using System;
using RhythmBase.Events;

namespace RhythmBase.Exceptions
{

	public class IllegalBeatException(IBarBeginningEvent item) : RhythmBaseException
	{

		public override string Message
		{
			get
			{
				return string.Format("This beat is invalid, the event {0} only allows the beat to be at the beginning of the bar.", ((BaseEvent)Item).Type);
			}
		}

		public IBarBeginningEvent Item = item;
	}
}
