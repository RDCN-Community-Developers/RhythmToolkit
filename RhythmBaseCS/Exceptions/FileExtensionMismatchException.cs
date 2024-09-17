using System;
namespace RhythmBase.Exceptions
{
	public class FileExtensionMismatchException : SpriteException
	{
		public FileExtensionMismatchException()
		{
		}

		public FileExtensionMismatchException(string message) : base(message)
		{
		}

		public FileExtensionMismatchException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
