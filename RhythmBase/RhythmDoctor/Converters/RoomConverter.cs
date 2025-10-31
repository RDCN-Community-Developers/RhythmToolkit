using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RoomConverter : JsonConverter<RDRoom>
	{
		public override RDRoom Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
			List<byte> rooms = [];
			while (reader.Read())
			{
				if(reader.TokenType != JsonTokenType.EndArray)
				{
					byte room = reader.GetByte();
					rooms.Add(room);
				}
			}
			if (reader.TokenType != JsonTokenType.EndArray)
				throw new JsonException($"Expected EndArray token, but got {reader.TokenType}.");
			reader.Read();
			RDRoom result = new();
			foreach(byte room in rooms)
			{
				result[room] = true;
			}
			return result;
		}

		public override void Write(Utf8JsonWriter writer, RDRoom value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			foreach (byte item in value.Rooms)
				writer.WriteNumberValue(item);
			writer.WriteEndArray();
		}
	}
	internal class SingleRoomConverter : JsonConverter<RDSingleRoom>
	{
		public override RDSingleRoom Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
			reader.Read();
			byte index = reader.GetByte();
			RDSingleRoom result = new(index);
			reader.Read();
			if (reader.TokenType != JsonTokenType.EndArray)
				throw new JsonException($"Expected EndArray token, but got {reader.TokenType}.");
			reader.Read();
			return result;
		}

		public override void Write(Utf8JsonWriter writer, RDSingleRoom value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			writer.WriteNumberValue(value.Value);
			writer.WriteEndArray();
		}
	}
}
