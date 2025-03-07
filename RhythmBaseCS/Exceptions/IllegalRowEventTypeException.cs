using RhythmBase.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Exceptions
{
	/// <summary>
	/// Represents an exception that is thrown when an illegal row event type is encountered.
	/// </summary>
	public class IllegalRowEventTypeException : RhythmBaseException
	{
		public EventType EventType { get; }
		public RowType RowType { get; }
		public override string Message => $"{EventType} is not legal for {RowType} row.";
		/// <summary>
		/// Initializes a new instance of the <see cref="IllegalRowEventTypeException"/> class.
		/// </summary>
		public IllegalRowEventTypeException(EventType eventType, RowType rowType)
		{
			EventType = eventType;
			RowType = rowType;
		}
	}
}
