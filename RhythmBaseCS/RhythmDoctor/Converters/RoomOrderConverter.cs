using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RoomOrderConverter : JsonConverter<RoomOrder>
	{
		public override RoomOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			byte[] bytes = new byte[4];
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
			for (int i = 0; i < 4; i++){
				reader.Read();
				bytes[i] = reader.GetByte();
			}
			reader.Read();
			if (reader.TokenType != JsonTokenType.EndArray)
				throw new JsonException($"Expected EndArray token, but got {reader.TokenType}.");
			return new(bytes[0], bytes[1], bytes[2], bytes[3]);
		}

		public override void Write(Utf8JsonWriter writer, RoomOrder value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			foreach (byte b in value.Order)
				writer.WriteNumberValue(b);
			writer.WriteEndArray();
		}
	}

}
