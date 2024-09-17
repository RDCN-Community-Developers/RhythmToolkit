using System;
namespace RhythmBase.Exceptions
{
	public class RhythmBaseException : Exception
	{
		public RhythmBaseException()
		{
		}

		public RhythmBaseException(string message) : base(message)
		{
		}

		public RhythmBaseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
