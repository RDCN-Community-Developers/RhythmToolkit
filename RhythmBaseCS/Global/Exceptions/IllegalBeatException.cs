using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Exception thrown when a beat is placed in an illegal position.
	/// </summary>
	public class IllegalBeatException(IBarBeginningEvent item) : RhythmBaseException
	{
		/// <summary>
		/// Gets the error message that explains the reason for the exception.
		/// </summary>
		public override string Message
		{
			get
			{
				return string.Format("This beat is invalid, the event {0} only allows the beat to be at the beginning of the bar.", ((BaseEvent)Item).Type);
			}
		}

		/// <summary>
		/// Gets the event that caused the exception.
		/// </summary>
		public IBarBeginningEvent Item = item;
	}
}
