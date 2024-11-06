using Newtonsoft.Json.Linq;
namespace RhythmBase.Exceptions
{
	/// <summary>
	/// Represents errors that occur during converting operations.
	/// </summary>
	public class ConvertingException : RhythmBaseException
	{
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
			: base(string.Format("An exception was thrown on reading the event. \"{0}\"", @event), innerException) { }
	}
}
