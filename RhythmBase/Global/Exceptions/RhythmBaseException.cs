namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Represents errors that occur during application execution in the RhythmBase application.
	/// </summary>
	public class RhythmBaseException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RhythmBaseException"/> class.
		/// </summary>
		public RhythmBaseException()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="RhythmBaseException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public RhythmBaseException(string message) : base(message)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="RhythmBaseException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public RhythmBaseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
