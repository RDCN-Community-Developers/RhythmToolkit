using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.RhythmDoctor.Converters;

internal class ExpressionConverter : JsonConverter<RDExpression>
{
	public override void Write(Utf8JsonWriter writer, RDExpression value, JsonSerializerOptions serializer)
	{
		if (value.IsNumeric)
			writer.WriteRawValue(value.NumericValue.ToString());
		else if (string.IsNullOrEmpty(value.ExpressionValue))
			writer.WriteNullValue();
		else
			writer.WriteStringValue($"{{{value.ExpressionValue}}}");
	}
	public override RDExpression Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
	{
		if (reader.TokenType == JsonTokenType.Number)
			return new RDExpression(reader.GetSingle());
		else return reader.TokenType == JsonTokenType.String
			? new RDExpression(reader.GetString()?.TrimStart('{').TrimEnd('}') ?? string.Empty)
			: default;
	}
}
