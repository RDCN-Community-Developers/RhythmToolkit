using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Extensions;

using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class PanelColorConverter : JsonConverter<PaletteColor>
	{
		internal PanelColorConverter(RDColor[] list)
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
				string s = value.Value.ToString().Replace("#", "");
				string alpha = s[..2];
				string rgb = s[2..];
				if (value.EnableAlpha)
				{
					writer.WriteValue(rgb + alpha);
				}
				else
				{
					writer.WriteValue(rgb);
				}
			}
		}

		public override PaletteColor ReadJson(JsonReader reader, Type objectType, PaletteColor? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			string? JString = token.Value<string>();
			if (JString.IsNullOrEmpty())
			{
				throw new Exceptions.ConvertingException(token, new Exception($"Unreadable color: \"{token}\". path \"{reader.Path}\""));
			}
			Match reg = PaletteColorRegex().Match(JString);
			existingValue!.parent = parent;
			if (reg.Success)
			{
				existingValue.PaletteIndex = int.Parse(reg.Groups[1].Value);
			}
			else
			{
				string s = JString.Replace("#", "");
				string alpha = "";
				if (s.Length > 6)
				{
					alpha = s[6..];
				}
				string rgb = s[..6];
				if (s.Length > 6)
				{
					existingValue.Color = new RDColor?(RDColor.FromArgb(alpha + rgb));
				}
				else
				{
					existingValue.Color = new RDColor?(RDColor.FromRgba(rgb));
				}
			}
			return existingValue;
		}

		private readonly RDColor[] parent;

		[GeneratedRegex("pal(\\d+)")]
		private static partial Regex PaletteColorRegex();
	}
}
