using System;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using SkiaSharp;
namespace RhythmBase.Converters
{
	internal class PanelColorConverter : JsonConverter<PaletteColor>
	{
		internal PanelColorConverter(LimitedList<SKColor> list)
		{
			parent = list;
		}

		public override void WriteJson(JsonWriter writer, PaletteColor value, JsonSerializer serializer)
		{
			bool enablePanel = value.EnablePanel;
			if (enablePanel)
			{
				writer.WriteValue(string.Format("pal{0}", value.PaletteIndex));
			}
			else
			{
				string s = value.Value.ToString().Replace("#", "");
				string alpha = s.Substring(0, 2);
				string rgb = s.Substring(2);
				bool enableAlpha = value.EnableAlpha;
				if (enableAlpha)
				{
					writer.WriteValue(rgb + alpha);
				}
				else
				{
					writer.WriteValue(rgb);
				}
			}
		}

		public override PaletteColor ReadJson(JsonReader reader, Type objectType, PaletteColor existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string JString = JToken.Load(reader).Value<string>();
			Match reg = Regex.Match(JString, "pal(\\d+)");
			existingValue.parent = parent;
			bool success = reg.Success;
			if (success)
			{
				existingValue.PaletteIndex = Conversions.ToInteger(reg.Groups[1].Value);
			}
			else
			{
				string s = JString.Replace("#", "");
				string alpha = "";
				bool flag = s.Length > 6;
				if (flag)
				{
					alpha = s.Substring(6);
				}
				string rgb = s.Substring(0, 6);
				bool flag2 = s.Length > 6;
				if (flag2)
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
