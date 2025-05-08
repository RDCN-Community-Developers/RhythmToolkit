using Newtonsoft.Json.Linq;

namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Represents errors that occur during converting operations.
	/// </summary>
	public class ConvertingException : RhythmBaseException
	{
#pragma warning disable IDE0052 // 删除未读的私有成员
		private readonly JToken? _convertingEvent;
#pragma warning restore IDE0052 // 删除未读的私有成员
		/// <summary>
		/// Initializes a new instance of the <see cref="ConvertingException"/> class with a specified inner exception.
		/// </summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public ConvertingException(Exception innerException)
			: base("An exception was thrown on reading the level.", innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConvertingException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ConvertingException(string message)
			: base(string.Format("An exception was thrown on reading the event: {0}", message)) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConvertingException"/> class with a specified event and inner exception.
		/// </summary>
		/// <param name="event">The event that caused the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public ConvertingException(JToken @event, Exception innerException)
			: base($"An exception was thrown on reading the event. \"{innerException}\"")
		{
			_convertingEvent = @event;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ConvertingException"/> class with a specified event and inner exception.
		/// </summary>
		/// <param name="event">The event that caused the exception.</param>
		/// <param name="message">The exception that is the cause of the current exception.</param>
		public ConvertingException(JToken @event, string message)
			: base($"An exception was thrown on reading the event: {message}")
		{
			_convertingEvent = @event;
		}
	}
}
