using RhythmBase.Adofai.Events;
using RhythmBase.Global.Extensions;
using System.Text;
using System.Text.Json;

namespace RhythmBase.Adofai.Converters;
internal abstract class EventInstanceConverterBase
{
	public abstract IBaseEvent ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options);
	public abstract void WriteProperties(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options);
}
internal abstract class EventInstanceConverterBaseEvent<TEvent> : EventInstanceConverterBase where TEvent : IBaseEvent, new()
{
	public override sealed IBaseEvent ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		TEvent value = new();
		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
			{
				return value;
			}
			if (reader.TokenType != JsonTokenType.PropertyName)
				throw new JsonException("Expected property name");
			ReadOnlySpan<byte> propertyName = reader.ValueSpan;
			if (propertyName.IsEmpty)
				throw new JsonException("Property name cannot be null");
			reader.Read();
			if (propertyName.SequenceEqual("eventType"u8))
				continue;
			else if (!Read(ref reader, propertyName, ref value, options))
			{
#if DEBUG
				if (!(false
					))
					Console.WriteLine($"The key {Encoding.UTF8.GetString([.. propertyName])} of {value.Type} not found.");
#endif
				value[
#if NET8_0_OR_GREATER
					Encoding.UTF8.GetString(propertyName)
#elif NETSTANDARD2_0_OR_GREATER
						Encoding.UTF8.GetString(propertyName.ToArray())
#endif
					] = JsonElement.ParseValue(ref reader);
			}
		}
		return value;
	}
	public override sealed void WriteProperties(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options)
	{
		TEvent v = (TEvent)value;
		writer.WriteStartObject();
		Write(writer, ref v, options);
		foreach (var kv in ((BaseEvent)(IBaseEvent)v)._extraData)
		{
			writer.WritePropertyName(kv.Key);
			writer.WriteRawValue(kv.Value.GetRawText());
		}
		writer.WriteEndObject();
	}
	protected virtual bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		bool result = false;
		return result;
	}
	protected virtual void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		if (value is BaseTileEvent bte && bte._floor >= 0)
			writer.WriteNumber("floor"u8, bte._floor);
		writer.WriteString("eventType"u8, EnumConverter.ToEnumString(value.Type));
		if (value is BaseTaggedTileEvent btta)
		{
			if (btta.AngleOffset != 0)
				writer.WriteNumber("angleOffset"u8, btta.AngleOffset);
			if (!string.IsNullOrEmpty(btta.EventTag))
				writer.WriteString("eventTag"u8, btta.EventTag);
		}
	}
}