using System.Text.Json;
using System.Text.Json.Serialization;
using RhythmBase.Adofai.Components;
using RhythmBase.Global.Extensions;

namespace RhythmBase.Adofai.Converters
{
	internal class TileReferenceConverter : JsonConverter<TileReference>
	{
		public override TileReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
			reader.Read();
			if (reader.TokenType != JsonTokenType.Number)
				throw new JsonException($"Expected Number token for Offset, but got {reader.TokenType}.");
			int offset = reader.GetInt32();
			reader.Read();
			if (reader.TokenType != JsonTokenType.String)
				throw new JsonException($"Expected String token for Type, but got {reader.TokenType}.");
			ReadOnlySpan<byte> typeSpan = reader.ValueSpan;
			if (!EnumConverter.TryParse(typeSpan, out RelativeType type))
				throw new JsonException($"Invalid RelativeType value: {typeSpan.ToString()}");
			reader.Read();
			if (reader.TokenType != JsonTokenType.EndArray)
				throw new JsonException($"Expected EndArray token, but got {reader.TokenType}.");
			reader.Read();
			return new TileReference
			{
				Offset = offset,
				Type = type
			};
		}
		public override void Write(Utf8JsonWriter writer, TileReference value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			writer.WriteNumberValue(value.Offset);
			writer.WriteStringValue(EnumConverter.ToEnumString(value.Type));
			writer.WriteEndArray();
		}
	}
}
