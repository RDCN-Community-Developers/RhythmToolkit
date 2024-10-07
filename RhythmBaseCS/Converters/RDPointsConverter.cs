using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Extensions;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Converters
{
	internal class RDPointsConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, [NotNull] object? value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			Type type = value!.GetType();
			if (value is RDPointNI ||
				value is RDPointNI?)
			{
				writer.WriteValue(((RDPointNI)value).X);
				writer.WriteValue(((RDPointNI)value).Y);
			}
			else if (
				value is RDPointN ||
				value is RDPointN?)
			{
				writer.WriteValue(((RDPointN)value).X);
				writer.WriteValue(((RDPointN)value).Y);
			}
			else if (
				value is RDPointI ||
				value is RDPointI?)
			{
				writer.WriteValue(((RDPointI)value).X);
				writer.WriteValue(((RDPointI)value).Y);
			}
			else if (
				value is RDPoint ||
				value is RDPoint?)
			{
				writer.WriteValue(((RDPoint)value).X);
				writer.WriteValue(((RDPoint)value).Y);
			}
			else if (value is RDPointE ||
				value is RDPointE?)
			{
				RDPointE temp = (value != null) ? ((RDPointE)value) : default;
				if (temp.X != null)
					writer.WriteValue(temp.X.Value.IsNumeric ? temp.X.Value.NumericValue : temp.X.Value.ExpressionValue);
				else
					writer.WriteNull();
				if (temp.Y != null)
					writer.WriteValue(temp.Y.Value.IsNumeric ? temp.Y.Value.NumericValue : temp.Y.Value.ExpressionValue);
				else
					writer.WriteNull();
			}
			else if (value is RDSizeNI ||
				value is RDSizeNI?)
			{
				writer.WriteValue(((RDSizeNI)value).Width);
				writer.WriteValue(((RDSizeNI)value).Height);
			}
			else if (
				value is RDSizeN ||
				value is RDSizeN?)
			{
				writer.WriteValue(((RDSizeN)value).Width);
				writer.WriteValue(((RDSizeN)value).Height);
			}
			else if (
				value is RDSizeI ||
				value is RDSizeI?)
			{
				writer.WriteValue(((RDSizeI)value).Width);
				writer.WriteValue(((RDSizeI)value).Height);
			}
			else if (
				value is RDSize ||
				value is RDSize?)
			{
				writer.WriteValue(((RDSize)value).Width);
				writer.WriteValue(((RDSize)value).Height);
			}
			else if (!(value is RDSizeE ||
				value is RDSizeE?))
			{
				RDSizeE temp2 = (value != null) ? ((RDSizeE)value) : default;
				if (temp2.Width != null)
					writer.WriteValue(temp2.Width.Value.IsNumeric ? temp2.Width.Value.NumericValue : temp2.Width.Value.ExpressionValue);
				else
					writer.WriteNull();
				if (temp2.Height != null)
					writer.WriteValue(temp2.Height.Value.IsNumeric ? temp2.Height.Value.NumericValue : temp2.Height.Value.ExpressionValue);
				else
					writer.WriteNull();
			}
			else
				throw new NotImplementedException();
			writer.WriteEndArray();

		}
		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			JToken ja = JToken.ReadFrom(reader);
			object ReadJson;
			if (objectType == typeof(RDPointNI) || objectType == typeof(RDPointNI?))
				ReadJson = new RDPointNI(ja[0]!.ToObject<int>(), ja[1]!.ToObject<int>());
			else if (objectType == typeof(RDPointN) || objectType == typeof(RDPointN?))
				ReadJson = new RDPointN(ja[0]!.ToObject<float>(), ja[1]!.ToObject<float>());
			else if (objectType == typeof(RDPointI) || objectType == typeof(RDPointI?))
				ReadJson = new RDPointI(ja[0]?.ToObject<int?>(), ja[1]?.ToObject<int?>());
			else if (objectType == typeof(RDPoint) || objectType == typeof(RDPoint?))
				ReadJson = new RDPoint(ja[0]?.ToObject<float?>(), ja[1]?.ToObject<float?>());
			else if (objectType == typeof(RDPointE) || objectType == typeof(RDPointE?))
				ReadJson = new RDPointE(new RDExpression?(ja[0]!.ToString().IsNullOrEmpty()
		? default
		: ja[0]!.ToObject<RDExpression>()), new RDExpression?(ja[1]!.ToString().IsNullOrEmpty()
			? default
			: ja[1]!.ToObject<RDExpression>()));
			else if (objectType == typeof(RDSizeNI) || objectType == typeof(RDSizeNI?))
				ReadJson = new RDSizeNI(ja[0]!.ToObject<int>(), ja[1]!.ToObject<int>());
			else if (objectType == typeof(RDSizeN) || objectType == typeof(RDSizeN?))
				ReadJson = new RDSizeN(ja[0]!.ToObject<float>(), ja[1]!.ToObject<float>());
			else if (objectType == typeof(RDSizeI) || objectType == typeof(RDSizeI?))
				ReadJson = new RDSizeI(ja[0]?.ToObject<int?>(), ja[1]?.ToObject<int?>());
			else if (objectType == typeof(RDSize) || objectType == typeof(RDSize?))
				ReadJson = new RDSize(ja[0]?.ToObject<float?>(), ja[1]?.ToObject<float?>());
			else if (objectType == typeof(RDSizeE) || objectType == typeof(RDSizeE?))
				ReadJson = new RDSizeE(new RDExpression?(ja[0]!.ToString().IsNullOrEmpty()
		? default
		: ja[0]!.ToObject<RDExpression>()), new RDExpression?(ja[1]!.ToString().IsNullOrEmpty()
			? default
			: ja[1]!.ToObject<RDExpression>()));
			else
				throw new NotImplementedException();
			return ReadJson;
		}
		public override bool CanConvert(Type objectType) => throw new NotImplementedException();
	}
}
