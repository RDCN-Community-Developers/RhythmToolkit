using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Events;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal class AnchorStyleConverter : JsonConverter<FloatingText.AnchorStyle>
	{
		public override void WriteJson(JsonWriter writer, FloatingText.AnchorStyle value, JsonSerializer serializer)
		{
			writer.WriteValue((((value & (FloatingText.AnchorStyle.Lower | FloatingText.AnchorStyle.Upper)) > FloatingText.AnchorStyle.Center) ? Enum.Parse<FloatingText.AnchorStyle>(Conversions.ToString((int)(value & (FloatingText.AnchorStyle.Lower | FloatingText.AnchorStyle.Upper)))).ToString() : "Middle") + Enum.Parse<FloatingText.AnchorStyle>(Conversions.ToString((int)(value & (FloatingText.AnchorStyle.Left | FloatingText.AnchorStyle.Right)))).ToString());
		}

		public override FloatingText.AnchorStyle ReadJson(JsonReader reader, Type objectType, FloatingText.AnchorStyle existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string JString = JToken.ReadFrom(reader).ToObject<string>() ?? throw new Exceptions.ConvertingException("Cannot read the anchor.");
			Match match = Regex.Match(JString, "(Upper|Middle|Lower)(Left|Center|Right)");
			if (!match.Success)
				throw new Exceptions.ConvertingException($"Illegal Anchor: {JString}");
			FloatingText.AnchorStyle result = FloatingText.AnchorStyle.Center;
			string value = match.Groups[1].Value;
			switch (value)
			{
				case "Upper":
					result |= FloatingText.AnchorStyle.Upper;
					break;
				case "Lower":
					result |= FloatingText.AnchorStyle.Lower;
					break;
			}

			string value2 = match.Groups[2].Value;
			switch (value2)
			{
				case "Left":
					result |= FloatingText.AnchorStyle.Left;
					break;
				case "Right":
					result |= FloatingText.AnchorStyle.Right;
					break;
			}
			return result;
		}
	}
}
