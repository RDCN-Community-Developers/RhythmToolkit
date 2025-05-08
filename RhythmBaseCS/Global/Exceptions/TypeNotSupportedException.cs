﻿namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Exception thrown when a type is not supported.
	/// </summary>
	[Serializable]
	internal class TypeNotSupportedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeNotSupportedException"/> class.
		/// </summary>
		public TypeNotSupportedException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeNotSupportedException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public TypeNotSupportedException(string? message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeNotSupportedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public TypeNotSupportedException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}