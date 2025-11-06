using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class PaletteColorConverter : JsonConverter<PaletteColor>
	{
		public override PaletteColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ReadOnlySpan<byte> t = reader.ValueSpan;
			if (t.Length == 0) return default;
#if NETSTANDARD
			return (t[0] is 0x70 &&
				t[1] is 0x61 &&
				t[2] is 0x6c &&
				t.Length > 3)
				? new() { PaletteIndex = (t.Length switch { 
					4 => t[3] is >= (byte)'0' and <= (byte)'9'
						? t[3] - '0'
						: throw new Exception(""),
					5 => t[3] is >= (byte)'0' and <= (byte)'9' && t[4] is >= (byte)'0' and <= (byte)'9'
						? (t[3] - '0') * 10 + (t[4] - '0')
						: throw new Exception(""),
					_ => throw new Exception(""),
				}) }
				: new() { Color = RDColor.FromRgba(t) };
#else
			return (t is [0x70, 0x61, 0x6c, ..])
				? new() { PaletteIndex = int.Parse(t[3..]) }
				: new(t.Length == 8) { Color = RDColor.FromRgba(t) };
#endif
			//			var s = reader.GetString();
			//			if (string.IsNullOrEmpty(s)) return default;
			//			return s.StartsWith("pal")
			//#if NETSTANDARD
			//				? new() { PaletteIndex = int.Parse(s.Substring(3)) }
			//#else
			//				? new() { PaletteIndex = int.Parse(s[3..]) }
			//#endif
			//				: new() { Color = RDColor.FromRgba(s) };
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
