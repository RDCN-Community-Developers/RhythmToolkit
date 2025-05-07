namespace RhythmBase.Exceptions
{
	/// <summary>
	/// Exception thrown when a file extension does not match the expected extension.
	/// </summary>
	public class FileExtensionMismatchException : SpriteException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FileExtensionMismatchException"/> class.
		/// </summary>
		public FileExtensionMismatchException()
		{
		}		/// <summary>
		/// Initializes a new instance of the <see cref="FileExtensionMismatchException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FileExtensionMismatchException(string message) : base(message)
		{
		}		/// <summary>
		/// Initializes a new instance of the <see cref="FileExtensionMismatchException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public FileExtensionMismatchException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
