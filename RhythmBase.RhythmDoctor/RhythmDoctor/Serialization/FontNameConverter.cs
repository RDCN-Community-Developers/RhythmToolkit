using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Serialization;

[JsonConverterFor(typeof(FontName))]
internal class FontNameConverter : JsonConverter<FontName>
{
	public override FontName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		JsonException.ThrowIfNotMatch(ref reader, JsonTokenType.String);
		string fontName = reader.GetString() ?? string.Empty;
		return fontName;
	}

	public override void Write(Utf8JsonWriter writer, FontName value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.Value);
	}
}
