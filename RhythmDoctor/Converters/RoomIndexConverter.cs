using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RoomIndexConverter : JsonConverter<RDRoomIndex>
	{
		public override RDRoomIndex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.Number)
				throw new JsonException($"Expected Number token, but got {reader.TokenType}.");
			byte index = reader.GetByte();
			if(index > 4)
				throw new JsonException($"Room index must be between 0 and 4, but got {index}.");
			return (RDRoomIndex)(1 << index);
		}

		public override void Write(Utf8JsonWriter writer, RDRoomIndex value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value switch
			{
				RDRoomIndex.None => 0,
				RDRoomIndex.Room1 => 1,
				RDRoomIndex.Room2 => 2,
				RDRoomIndex.Room3 => 3,
				RDRoomIndex.Room4 => 4,
				_ => throw new JsonException($"Cannot convert {value} to room index."),
			});
		}
	}
}
