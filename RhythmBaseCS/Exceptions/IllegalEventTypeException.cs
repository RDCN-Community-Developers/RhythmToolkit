﻿using System;
using RhythmBase.Extensions;
namespace RhythmBase.Exceptions
{
	public class IllegalEventTypeException : RhythmBaseException
	{
		public override string Message
		{
			get
			{
				return string.Format("Illegal type: \"{0}\"{1}", IllegalTypeName, ExtraMessage.IsNullOrEmpty() ? "." : string.Format(", {0}", ExtraMessage));
			}
		}

		public string ExtraMessage { get; }

		public string IllegalTypeName { get; }

		public IllegalEventTypeException(Type type) : this(type, string.Empty)
		{
		}

		public IllegalEventTypeException(string type) : this(type, string.Empty)
		{
		}

		public IllegalEventTypeException(Type type, string extraMessage)
		{
			IllegalTypeName = type.Name;
			ExtraMessage = extraMessage;
		}

		public IllegalEventTypeException(string type, string extraMessage)
		{
			IllegalTypeName = type;
			ExtraMessage = extraMessage;
		}

		public IllegalEventTypeException(Type type, string extraMessage, Exception innerException) : base("", innerException)
		{
			IllegalTypeName = type.Name;
			ExtraMessage = extraMessage;
		}

		public IllegalEventTypeException(string type, string extraMessage, Exception innerException) : base("", innerException)
		{
			IllegalTypeName = type;
			ExtraMessage = extraMessage;
		}
	}
}
