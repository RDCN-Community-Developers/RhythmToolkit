using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters;

internal class PaletteColorWithAlphaConverter : JsonConverter<PaletteColorWithAlpha>
{
	public override PaletteColorWithAlpha Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			? new(int.Parse(t[3..]))
			: new(RDColor.FromRgba(t));
#endif
	}

	public override void Write(Utf8JsonWriter writer, PaletteColorWithAlpha value, JsonSerializerOptions options)
	{
		if (value.EnablePanel)
		{
			writer.WriteStringValue($"pal{value.PaletteIndex}");
		}
		else
		{
			writer.WriteStringValue(value.Value.ToString("RRGGBBAA"));
		}
	}
}
