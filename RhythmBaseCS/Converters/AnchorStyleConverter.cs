﻿using System;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Events;
namespace RhythmBase.Converters
{
	internal class AnchorStyleConverter : JsonConverter<FloatingText.AnchorStyle>
	{
		public override void WriteJson(JsonWriter writer, FloatingText.AnchorStyle value, JsonSerializer serializer)
		{
			string text = ((value & (FloatingText.AnchorStyle.Lower | FloatingText.AnchorStyle.Upper)) > FloatingText.AnchorStyle.Center) ? "Middle" : Enum.Parse<FloatingText.AnchorStyle>(Conversions.ToString((int)(value & (FloatingText.AnchorStyle.Lower | FloatingText.AnchorStyle.Upper)))).ToString();
			writer.WriteValue((((value & (FloatingText.AnchorStyle.Lower | FloatingText.AnchorStyle.Upper)) > FloatingText.AnchorStyle.Center) ? Enum.Parse<FloatingText.AnchorStyle>(Conversions.ToString((int)(value & (FloatingText.AnchorStyle.Lower | FloatingText.AnchorStyle.Upper)))).ToString() : "Middle") + Enum.Parse<FloatingText.AnchorStyle>(Conversions.ToString((int)(value & (FloatingText.AnchorStyle.Left | FloatingText.AnchorStyle.Right)))).ToString());
		}

		public override FloatingText.AnchorStyle ReadJson(JsonReader reader, Type objectType, FloatingText.AnchorStyle existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string JString = JToken.ReadFrom(reader).ToObject<string>();
			Match match = Regex.Match(JString, "([A-Z][a-z]+)([A-Z][a-z]+)");
			bool middle = false;
			bool center = false;
			FloatingText.AnchorStyle result = FloatingText.AnchorStyle.Center;
			string value = match.Groups[1].Value;
			if (Operators.CompareString(value, "Upper", false) != 0)
			{
				if (Operators.CompareString(value, "Lower", false) != 0)
				{
					middle = true;
				}
				else
				{
					result |= FloatingText.AnchorStyle.Lower;
				}
			}
			else
			{
				result |= FloatingText.AnchorStyle.Upper;
			}
			string value2 = match.Groups[2].Value;
			if (Operators.CompareString(value2, "Left", false) != 0)
			{
				if (Operators.CompareString(value2, "Right", false) != 0)
				{
					center = true;
				}
				else
				{
					result |= FloatingText.AnchorStyle.Right;
				}
			}
			else
			{
				result |= FloatingText.AnchorStyle.Left;
			}
			if (center && middle)
			{
				result = FloatingText.AnchorStyle.Center;
			}
			return result;
		}
	}
}