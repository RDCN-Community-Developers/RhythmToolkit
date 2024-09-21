using System;
namespace RhythmBase.Exceptions
{
	public class InvalidRDBeatException : RhythmBaseException
	{
		public InvalidRDBeatException()
		{
			Message = "The beat is invalid, possibly because the beat is not associated with the RDLevel.";
		}

		public override string Message { get; }
	}
}
