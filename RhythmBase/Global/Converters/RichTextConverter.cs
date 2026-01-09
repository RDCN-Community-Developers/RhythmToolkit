using RhythmBase.Global.Components.RichText;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.Global.Converters;

internal class RichTextConverter<TRichTextStyle> : JsonConverter<RDLine<TRichTextStyle>> where TRichTextStyle : IRDRichStringStyle<TRichTextStyle>, new()
{
	public override RDLine<TRichTextStyle> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
#if NETSTANDARD2_0
		return RDLine<TRichTextStyle>.Empty.Deserialize(reader.GetString() ?? string.Empty);
#else
		return RDLine<TRichTextStyle>.Deserialize(reader.GetString() ?? string.Empty);
#endif
	}

	public override void Write(Utf8JsonWriter writer, RDLine<TRichTextStyle> value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.Serialize());
	}
}
