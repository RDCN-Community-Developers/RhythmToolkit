using System;
namespace RhythmBase.Exceptions
{
	public class SpriteException : Exception
	{
		public SpriteException()
		{
		}

		public SpriteException(string message) : base(message)
		{
		}

		public SpriteException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
