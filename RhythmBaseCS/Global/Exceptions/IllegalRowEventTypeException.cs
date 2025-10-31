using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Represents an exception that is thrown when an illegal row event type is encountered.
	/// </summary>
	public class IllegalRowEventTypeException : RhythmBaseException
	{
		/// <summary>
		/// Gets the event type that caused the exception.
		/// </summary>
		public EventType EventType { get; }
		/// <summary>
		/// Gets the row type that the event type is not legal for.
		/// </summary>
		public RowTypes RowType { get; }
		/// <summary>
		/// Gets the error message that explains the reason for the exception.
		/// </summary>
		public override string Message => $"{EventType} is not legal for {RowType} row.";
		/// <summary>
		/// Initializes a new instance of the <see cref="IllegalRowEventTypeException"/> class with the specified event type and row type.
		/// </summary>
		/// <param name="eventType">The event type that caused the exception.</param>
		/// <param name="rowType">The row type that the event type is not legal for.</param>
		public IllegalRowEventTypeException(EventType eventType, RowTypes rowType)
		{
			EventType = eventType;
			RowType = rowType;
		}
	}
}
