using RhythmBase.Adofai.Events;
using RhythmBase.Global.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RhythmBase.Adofai.Utils.EventTypeUtils;
namespace RhythmBase.Adofai.Converters
{
	internal class BaseEventConverter : JsonConverter<IBaseEvent>
	{
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
					if (reader.ValueSpan.SequenceEqual("eventType"u8))
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
			if (reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException($"Expected EndObject token, but got {reader.TokenType}.");
			reader.Read();
			return e;
		}
		public override void Write(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options)
		{
			if(value is IForwardEvent forwardEvent)
			{
				WriteForwardEvent(writer, forwardEvent);
				return;
			}
			converters[value.Type].WriteProperties(writer, value, options);
		}
		public IForwardEvent? ReadForwardEvent(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using JsonDocument doc = JsonDocument.ParseValue(ref reader);
			JsonElement root = doc.RootElement;

			// 判断属性
			bool isTile = false;
			foreach (JsonProperty prop in root.EnumerateObject())
			{
				if (prop.NameEquals("floor"))
					isTile = true;
			}
			if (isTile) return new ForwardTileEvent(doc);
			else return new ForwardEvent(doc);
		}
		public static void WriteForwardEvent(Utf8JsonWriter writer, IForwardEvent value)
		{
			writer.WriteStartObject();
			writer.WriteString("eventType", value.ActureType);
			if (value is ForwardTileEvent tileEvent)
				writer.WriteNumber("floor", tileEvent._floor);
			value.Data.WriteTo(writer);
			writer.WriteEndObject();
		}

	}
}