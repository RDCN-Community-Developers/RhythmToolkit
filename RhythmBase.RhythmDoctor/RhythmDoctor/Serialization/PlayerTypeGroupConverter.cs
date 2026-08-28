using RhythmBase.Global.Serialization;
using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Serialization;

[JsonConverterFor(typeof(PlayerTypeGroup))]
internal class PlayerTypeGroupConverter : MetadataJsonConverter<PlayerTypeGroup>
{
	public override PlayerTypeGroup Read(ref Utf8JsonReader reader, Type typeToConvert, MetadataJsonSerializerOptions options)
	{
		JsonException.ThrowIfNotMatch(ref reader, JsonTokenType.StartArray);
		PlayerTypeGroup group = new();
		int i = 0;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndArray && i < 16)
		{
			JsonException.ThrowIfNotMatch(ref reader, JsonTokenType.String);
			group[i++] = EnumConverter.TryParse(ref reader, out PlayerType type) ? type : PlayerType.NoChange;
		}
		while (reader.TokenType != JsonTokenType.EndArray)
			reader.Read();
		return group;
	}

	public override void Write(Utf8JsonWriter writer, PlayerTypeGroup value, MetadataJsonSerializerOptions options)
	{
		writer.WriteStartArray();
		for (int i = 0; i < 16; i++)
			writer.WriteStringValue(value[i].ToEnumString());
		writer.WriteEndArray();
	}
}
