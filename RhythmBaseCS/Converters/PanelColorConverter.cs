using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Extensions;
using SkiaSharp;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal class PanelColorConverter : JsonConverter<PaletteColor>
	{
		internal PanelColorConverter(LimitedList<SKColor> list)
		{
			parent = list;
		}

		public override void WriteJson(JsonWriter writer, PaletteColor? value, JsonSerializer serializer)
		{
			if (value.EnablePanel)
			{
				writer.WriteValue(string.Format("pal{0}", value.PaletteIndex));
			}
			else
			{
				string s = value.Value.ToString().Replace("#", "");
				string alpha = s.Substring(0, 2);
				string rgb = s.Substring(2);
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
            Match reg = Regex.Match(JString, "pal(\\d+)");
			existingValue.parent = parent;
			if (reg.Success)
			{
				existingValue.PaletteIndex = Conversions.ToInteger(reg.Groups[1].Value);
			}
			else
			{
				string s = JString.Replace("#", "");
				string alpha = "";
				if (s.Length > 6)
				{
					alpha = s.Substring(6);
				}
				string rgb = s.Substring(0, 6);
				if (s.Length > 6)
				{
					existingValue.Color = new SKColor?(SKColor.Parse(alpha + rgb));
				}
				else
				{
					existingValue.Color = new SKColor?(SKColor.Parse(rgb));
				}
			}
			return existingValue;
		}

		private readonly LimitedList<SKColor> parent;
	}
}
