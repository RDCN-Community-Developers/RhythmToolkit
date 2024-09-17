using System;
using Newtonsoft.Json.Linq;
namespace RhythmBase.Exceptions
{
	public class ConvertingException : RhythmBaseException
	{
		public ConvertingException(Exception innerException) : base("An exception was thrown on reading the level.", innerException)
		{
		}

		public ConvertingException(string message) : base(string.Format("An exception was thrown on reading the event: {0}", message))
		{
		}

		public ConvertingException(JObject @event, Exception innerException) : base(string.Format("An exception was thrown on reading the event. \"{0}\"", @event), innerException)
		{
		}
	}
}
