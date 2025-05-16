using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Components;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.Global.Converters
{
	internal class RDPointsConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			if (value is RDPointNI v1)
			{
				writer.WriteValue(v1.X);
				writer.WriteValue(v1.Y);
			}
			else if (value is RDPointN v2)
			{
				writer.WriteValue(v2.X);
				writer.WriteValue(v2.Y);
			}
			else if (value is RDPointI v3)
			{
				writer.WriteValue(v3.X);
				writer.WriteValue(v3.Y);
			}
			else if (value is RDPoint v4)
			{
				writer.WriteValue(v4.X);
				writer.WriteValue(v4.Y);
			}
			else if (value is RDPointE v5)
			{
				if (v5.X != null)
					if (v5.X.Value.IsNumeric)
						writer.WriteRawValue(v5.X.Value.NumericValue.ToString("G"));
					else
						writer.WriteValue(v5.X.Value.ExpressionValue);
				else
					writer.WriteNull();
				if (v5.Y != null)
					if (v5.Y.Value.IsNumeric)
						writer.WriteRawValue(v5.Y.Value.NumericValue.ToString("G"));
					else
						writer.WriteValue(v5.Y.Value.ExpressionValue);
				else
					writer.WriteNull();
			}
			else if (value is RDSizeNI v6)
			{
				writer.WriteValue(v6.Width);
				writer.WriteValue(v6.Height);
			}
			else if (value is RDSizeN v7)
			{
				writer.WriteValue(v7.Width);
				writer.WriteValue(v7.Height);
			}
			else if (value is RDSizeI v8)
			{
				writer.WriteValue(v8.Width);
				writer.WriteValue(v8.Height);
			}
			else if (value is RDSize v9)
			{
				writer.WriteValue(v9.Width);
				writer.WriteValue(v9.Height);
			}
			else if (value is RDSizeE v10)
			{
				RDSizeE temp2 = value != null ? v10 : default;
				if (temp2.Width != null)
					if (temp2.Width.Value.IsNumeric)
						writer.WriteRawValue(temp2.Width.Value.NumericValue.ToString("G"));
					else
						writer.WriteValue(temp2.Width.Value.ExpressionValue);
				else
					writer.WriteNull();
				if (temp2.Height != null)
					if (temp2.Height.Value.IsNumeric)
						writer.WriteRawValue(temp2.Height.Value.NumericValue.ToString("G"));
					else
						writer.WriteValue(temp2.Height.Value.ExpressionValue);
				else
					writer.WriteNull();
			}
			else
				throw new NotImplementedException();
			writer.WriteEndArray();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.Raw)
			{

			}
			JArray ja = (JArray)JToken.ReadFrom(reader);
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
