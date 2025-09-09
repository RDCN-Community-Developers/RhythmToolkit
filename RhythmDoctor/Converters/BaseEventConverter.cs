using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RhythmBase.RhythmDoctor.Utils.EventTypeUtils;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class BaseEventConverter : JsonConverter<IBaseEvent>
	{
		private delegate void PropertyAction(ref Utf8JsonReader reader);
		public override bool CanConvert(Type typeToConvert)
		{
			return Type.IsAssignableFrom(typeToConvert);
		}
		public override IBaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");

			ReadOnlySpan<byte> type = default;

			Utf8JsonReader checkpoint = reader;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueSpan.SequenceEqual("type"u8))
					{
						reader.Read();
						type = reader.ValueSpan;
						break;
					}
					else
					{
						reader.Skip();
					}
				}
			}
			reader = checkpoint; IBaseEvent e;
			if (!EnumConverter.TryParse(type, out EventType typeEnum))
				e = ReadForwardEvent(ref reader, typeToConvert, options) ?? new ForwardEvent() { ActureType = type.ToString() ?? "" };
			else
				e = converters[typeEnum].ReadProperties(ref reader, options);
			return e;
		}
		public override void Write(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options)
		{
			if (value is IForwardEvent ce)
			{
				WriteForwardEvent(writer, ce, options);
				return;
			}
			else if (value is MacroEvent group)
			{
				throw new NotSupportedException("Group should be handled in GroupConverter. It will be fixed in the next version.");
			}
			else
			{
				converters[value.Type].WriteProperties(writer, value, options);
			}
		}
		public IForwardEvent? ReadForwardEvent(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using var doc = JsonDocument.ParseValue(ref reader);
			var root = doc.RootElement;

			// 判断属性
			bool hasRow = false, hasTarget = false;
			foreach (var prop in root.EnumerateObject())
			{
				if (prop.NameEquals("row"))
					hasRow = true;
				else if (prop.NameEquals("target"))
					hasTarget = true;
			}
			if (hasRow) return new ForwardRowEvent(doc);
			else if (hasTarget) return new ForwardDecorationEvent(doc);
			else return new ForwardEvent(doc);
		}

		public void WriteForwardEvent(Utf8JsonWriter writer, IForwardEvent value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value.ActureType))
				writer.WriteString("type", value.ActureType);
			writer.WriteNumber("bar", value.Beat.BarBeat.bar);
			writer.WriteNumber("beat", value.Beat.BarBeat.beat);
			if (!string.IsNullOrEmpty(value.Tag))
				writer.WriteString("tag", value.Tag);
			if(value.RunTag)
				writer.WriteBoolean("runTag", value.RunTag);
			if (!value.Active)
				writer.WriteBoolean("active", value.Active);
			if (value.Condition is not null)
				writer.WriteString("condition", value.Condition.Serialize());
			if (value.Y != 0)
				writer.WriteNumber("y", value.Y);

			foreach (var kv in ((BaseEvent)value)._extraData)
			{
				writer.WritePropertyName(kv.Key);
				kv.Value.WriteTo(writer);
			}
			writer.WriteEndObject();
		}
	}
}