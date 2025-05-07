using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Extensions;
namespace RhythmBase.Converters
{
	internal class ExpressionConverter : JsonConverter<RDExpression>
	{
		public override void WriteJson(JsonWriter writer, RDExpression value, JsonSerializer serializer)
		{
			if (value.IsNumeric)
			{
				writer.WriteRawValue(value.NumericValue.ToString());
			}
			else
			{
				if (value.ExpressionValue.IsNullOrEmpty())
				{
					writer.WriteNull();
				}
				else
				{
					writer.WriteValue(string.Format("{{{0}}}", value.ExpressionValue));
				}
			}
		}		public override RDExpression ReadJson(JsonReader reader, Type objectType, RDExpression existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string js = JToken.ReadFrom(reader).ToObject<string>() ?? throw new RhythmBase.Exceptions.ConvertingException("Cannot read the expression.");
			RDExpression ReadJson = new(js.TrimStart('{').TrimEnd('}'));
			return ReadJson;
		}
	}
}
