using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class CpuTypeGroupConverter : JsonConverter<RDCharacters[]>
	{
		public override RDCharacters[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");
			reader.Read();
			RDCharacters[] group = new RDCharacters[16];
			for(int i = 0; i < 16; i++)
			{
				if(reader.TokenType == JsonTokenType.String)
				{
					group[i] = EnumConverter.TryParse(reader.ValueSpan, out RDCharacters type) ? type : RDCharacters.None;
				}
				else
				{
					throw new JsonException($"Expected String token, but got {reader.TokenType}.");
				}
				reader.Read();
			}
			return group;
		}

		public override void Write(Utf8JsonWriter writer, RDCharacters[] value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			for(int i = 0; i < 16; i++)
				writer.WriteStringValue(value[i].ToString());
			writer.WriteEndArray();
		}
	}
}
