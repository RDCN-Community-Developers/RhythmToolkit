namespace RhythmBase.Exceptions
{
	/// <summary>
	/// Represents errors that occur during expression evaluation in the RhythmBase application.
	/// </summary>
	public class ExpressionException : RhythmBaseException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExpressionException"/> class.
		/// </summary>
		public ExpressionException()
		{
		}		/// <summary>
		/// Initializes a new instance of the <see cref="ExpressionException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ExpressionException(string message) : base(message)
		{
		}
	}
}
