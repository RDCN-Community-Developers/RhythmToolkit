using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.Global.Converters
{
	internal class ColorConverter : JsonConverter<RDColor>
	{
		public override RDColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var s = reader.GetString();
			if (string.IsNullOrEmpty(s)) return default;
			if(RDColor.TryFromRgba(s!, out var c))
				return c;
			return default;
		}

		public override void Write(Utf8JsonWriter writer, RDColor value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("rrggbbaa"));
		}
	}
}
