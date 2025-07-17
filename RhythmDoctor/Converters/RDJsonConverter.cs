using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RDJsonConverter
	{
		private bool _hasFirstObject;
		public RDJsonConverter()
		{
		}
		public void WriteJson(StringWriter writer, IBaseEvent e)
		{
			writer.Write("{");
			if (e is not IBarBeginningEvent)
				WriteProperty(writer, "bar", e.Beat.BarBeat.bar);
			WriteProperty(writer, "beat", e.Beat.BarBeat.beat);
			WriteProperty(writer, "type", e.Type);
			_hasFirstObject = false;
			writer.Write("}");
		}
		private void WriteArray(StringWriter writer, IEnumerable<object> values)
		{
			writer.Write("[");
			bool first = true;
			foreach (var value in values)
			{
				if (!first)
					writer.Write(",");
				WriteObject(writer, value);
				first = false;
			}
			writer.Write("]");
		}
		private void WriteProperty(StringWriter writer, string propertyName, object value)
		{
			if (_hasFirstObject)
				writer.Write(",");
			writer.Write($"\"{propertyName}\":");
			WriteObject(writer, value);
			_hasFirstObject = true;
		}
		private void WriteObject(StringWriter writer, object value)
		{
			switch (value)
			{
				case null:
					writer.Write("null");
					break;
				case string or Enum:
					writer.Write($"\"{value}\"");
					break;
				case bool boolValue:
					writer.Write(boolValue ? "true" : "false");
					break;
				case int intValue:
					writer.Write(intValue);
					break;
				case float floatValue:
					writer.Write(floatValue);
					break;
				case double doubleValue:
					writer.Write(doubleValue);
					break;
				case decimal decimalValue:
					writer.Write(decimalValue);
					break;
				default:
					throw new NotImplementedException();
			}
		}
	}
}
