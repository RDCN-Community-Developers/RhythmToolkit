using System;
namespace RhythmBase.Exceptions
{
	public class InvalidRDBeatException : RhythmBaseException
	{
		public InvalidRDBeatException()
		{
			this.Message = "The beat is invalid, possibly because the beat is not associated with the RDLevel.";
		}

		public override string Message { get; }
	}
}
