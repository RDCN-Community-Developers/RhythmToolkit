using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkiaSharp;

namespace RhythmBase.Converters
{

	internal class ColorConverter : JsonConverter<SKColor>
	{

		public override void WriteJson(JsonWriter writer, SKColor value, JsonSerializer serializer)
		{
			string JString = value.ToString();
			Match Reg = Regex.Match(JString, "([0-9A-Fa-f]{2})?([0-9A-Fa-f]{6})");
			writer.WriteValue(Reg.Groups[1].Value + Reg.Groups[2].Value);
		}


		public override SKColor ReadJson(JsonReader reader, Type objectType, SKColor existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string JString = JToken.Load(reader).Value<string>();
			Match Reg = Regex.Match(JString, "([0-9A-Fa-f]{6})([0-9A-Fa-f]{2})?");
			return SKColor.Parse(Reg.Groups[1].Value + Reg.Groups[2].Value);
		}
	}
}
