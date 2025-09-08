using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class PaletteColorConverter : JsonConverter<PaletteColor>
	{
		public override PaletteColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var s = reader.GetString();
			if (string.IsNullOrEmpty(s)) return default;
			return s.StartsWith("pal")
#if NETSTANDARD
				? new() { PaletteIndex = int.Parse(s.Substring(3)) }
#else
				? new() { PaletteIndex = int.Parse(s[3..]) }
#endif
				: new() { Color = RDColor.FromRgba(s) };
		}

		public override void Write(Utf8JsonWriter writer, PaletteColor value, JsonSerializerOptions options)
		{
			if (value.EnablePanel)
			{
				writer.WriteStringValue($"pal{value.PaletteIndex}");
			}
			else
			{
				writer.WriteStringValue(value.EnableAlpha
					? value.Value.ToString("RRGGBBAA")
					: value.Value.ToString("RRGGBB"));
			}
		}
	}
}
