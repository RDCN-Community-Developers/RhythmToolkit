using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.Global.Converters;

[RDJsonConverterFor(typeof(RDColor))]
internal class ColorConverter : JsonConverter<RDColor>
{
	public override RDColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? s = reader.GetString();
		if (string.IsNullOrEmpty(s)) return default;
		return RDColor.TryFromRgba(s!, out RDColor c) ? c : default;
	}

	public override void Write(Utf8JsonWriter writer, RDColor value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString("rrggbbaa"));
	}
}
