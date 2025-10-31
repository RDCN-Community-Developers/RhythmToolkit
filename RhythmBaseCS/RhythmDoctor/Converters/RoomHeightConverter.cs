using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RoomHeightConverter : JsonConverter<RoomHeight>
	{
		public override RoomHeight Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");
			RoomHeight height = new RoomHeight();
			for (int i = 0; i < 4; i++)
			{
				reader.Read();
				if (reader.TokenType == JsonTokenType.Number)
				{
					height[i] = reader.GetInt32();
				}
				else
				{
					throw new JsonException($"Expected Number token, but got {reader.TokenType}.");
				}
			}
			reader.Read();
			if (reader.TokenType != JsonTokenType.EndArray)
				throw new JsonException($"Expected EndArray token, but got {reader.TokenType}.");
			reader.Read();
			return height;
		}

		public override void Write(Utf8JsonWriter writer, RoomHeight value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			for (int i = 0; i < 4; i++)
				writer.WriteNumberValue(value[i]);
			writer.WriteEndArray();
		}
	}
}
