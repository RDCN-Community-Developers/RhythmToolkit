using System;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Extensions;

namespace RhythmBase.Converters
{

	internal class ExpressionConverter : JsonConverter<Expression>
	{

		public override void WriteJson(JsonWriter writer, Expression value, JsonSerializer serializer)
		{
			bool isNumeric = value.IsNumeric;
			if (isNumeric)
			{
				writer.WriteRawValue(Conversions.ToString(value.NumericValue));
			}
			else
			{
				bool flag = value.ExpressionValue.IsNullOrEmpty();
				if (flag)
				{
					writer.WriteNull();
				}
				else
				{
					writer.WriteValue(string.Format("{{{0}}}", value.ExpressionValue));
				}
			}
		}


		public override Expression ReadJson(JsonReader reader, Type objectType, Expression existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string js = JToken.ReadFrom(reader).ToObject<string>();
			Expression ReadJson = new(js.TrimStart('{').TrimEnd('}'));
			return ReadJson;
		}
	}
}
