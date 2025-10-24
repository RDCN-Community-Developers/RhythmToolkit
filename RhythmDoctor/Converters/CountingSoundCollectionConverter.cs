using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class CountingSoundCollectionConverter : JsonConverter<RDAudio[]>
	{
		private static readonly AudioConverter audioConverter = new();
		public override RDAudio[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Unexpected token parsing CountingSoundCollection. Expected StartArray or Null, got {reader.TokenType}.");
			RDAudio[] audios = new RDAudio[7];
			int index = 0;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndArray)
				{
					if (index == 6)
						return audios;
					throw new JsonException("Not enough elements in CountingSoundCollection.");
				}
				if (index >= 7)
					throw new JsonException("Too many elements in CountingSoundCollection.");
				RDAudio? audio = audioConverter.Read(ref reader, typeof(RDAudio), options) ?? throw new JsonException("Null audio in CountingSoundCollection.");
				audios[index++] = audio;
			}
			throw new JsonException("Unexpected end of JSON while reading CountingSoundCollection.");
		}
		public override void Write(Utf8JsonWriter writer, RDAudio[] value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			for (int i = 0; i < 7; i++)
				audioConverter.Write(writer, value[i], options);
			writer.WriteEndArray();
		}
	}
}
