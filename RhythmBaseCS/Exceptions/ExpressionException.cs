using System;
namespace RhythmBase.Exceptions
{
	public class ExpressionException : RhythmBaseException
	{
		public ExpressionException()
		{
		}

		public ExpressionException(string message) : base(message)
		{
		}
	}
}
