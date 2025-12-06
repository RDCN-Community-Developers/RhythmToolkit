using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.RhythmDoctor.Converters;

internal class PatternConverter : JsonConverter<PatternCollection>
{
	public override PatternCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException($"Expected String token, but got {reader.TokenType}.");
		string s = reader.GetString() ?? "";
		return s;
	}
	public override void Write(Utf8JsonWriter writer, PatternCollection value, JsonSerializerOptions options) => writer.WriteStringValue(value);
}
