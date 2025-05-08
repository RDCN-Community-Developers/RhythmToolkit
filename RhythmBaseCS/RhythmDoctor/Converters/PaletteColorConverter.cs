using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.Global.Components;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Components;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class PaletteColorConverter : JsonConverter<PaletteColor>
	{
		internal PaletteColorConverter(RDColor[] list)
		{
			parent = list;
		}
		public override void WriteJson(JsonWriter writer, PaletteColor? value, JsonSerializer serializer)
		{
			if (value?.EnablePanel ?? throw new NotImplementedException())
			{
				writer.WriteValue(string.Format("pal{0}", value.PaletteIndex));
			}
			else
			{
				if (value.EnableAlpha)
				{
					writer.WriteValue(value.Value.ToString("RRGGBBAA"));
				}
				else
				{
					writer.WriteValue(value.Value.ToString("RRGGBB"));
				}
			}
		}
		public override PaletteColor ReadJson(JsonReader reader, Type objectType, PaletteColor? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (existingValue == null)
			{
				throw new ConvertingException(new Exception($"PaletteColorConverter: Inner exception: Property has no default value."));
			}
			JToken token = JToken.Load(reader);
			string? JString = token.Value<string>();
			if (JString.IsNullOrEmpty())
			{
				throw new ConvertingException(token, new Exception($"Unreadable color: \"{token}\". path \"{reader.Path}\""));
			}
			Match reg = PaletteColorRegex().Match(JString);
			existingValue.parent = parent;
			if (reg.Success)
			{
				existingValue.PaletteIndex = int.Parse(reg.Groups[1].Value);
			}
			else
			{
				existingValue.Color = RDColor.FromRgba(JString);
			}
			return existingValue;
		}
		private readonly RDColor[] parent;
		[GeneratedRegex("pal(\\d+)")]
		private static partial Regex PaletteColorRegex();
	}
}
