using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.Global.Converters
{
	internal class RDPointsConverter : JsonConverter<IRDVortex>
	{
		public RDPointsConverter() : base()
		{

		}
		public override void Write(Utf8JsonWriter writer, IRDVortex? value, JsonSerializerOptions serializer)
		{
			writer.WriteStartArray();
			if (value is RDPointNI v1)
			{
				writer.WriteNumberValue(v1.X);
				writer.WriteNumberValue(v1.Y);
			}
			else if (value is RDPointN v2)
			{
				writer.WriteNumberValue(v2.X);
				writer.WriteNumberValue(v2.Y);
			}
			else if (value is RDPointI v3)
			{
				if (v3.X is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v3.X.Value);
				if (v3.Y is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v3.Y.Value);
			}
			else if (value is RDPoint v4)
			{
				if (v4.X is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v4.X.Value);
				if (v4.Y is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v4.Y.Value);
			}
			else if (value is RDPointE v5)
			{
				if (v5.X != null)
					if (v5.X.Value.IsNumeric)
						writer.WriteNumberValue(v5.X.Value.NumericValue);
					else
						writer.WriteStringValue(v5.X.Value.ExpressionValue);
				else
					writer.WriteNullValue();
				if (v5.Y != null)
					if (v5.Y.Value.IsNumeric)
						writer.WriteNumberValue(v5.Y.Value.NumericValue);
					else
						writer.WriteStringValue(v5.Y.Value.ExpressionValue);
				else
					writer.WriteNullValue();
			}
			else if (value is RDSizeNI v6)
			{
				writer.WriteNumberValue(v6.Width);
				writer.WriteNumberValue(v6.Height);
			}
			else if (value is RDSizeN v7)
			{
				writer.WriteNumberValue(v7.Width);
				writer.WriteNumberValue(v7.Height);
			}
			else if (value is RDSizeI v8)
			{
				if (v8.Width is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v8.Width.Value);
				if (v8.Height is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v8.Height.Value);
			}
			else if (value is RDSize v9)
			{
				if (v9.Width is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v9.Width.Value);
				if (v9.Height is null)
					writer.WriteNullValue();
				else
					writer.WriteNumberValue(v9.Height.Value);
			}
			else if (value is RDSizeE v10)
			{
				if (v10.Width != null)
					if (v10.Width.Value.IsNumeric)
						writer.WriteNumberValue(v10.Width.Value.NumericValue);
					else
						writer.WriteStringValue(v10.Width.Value.ExpressionValue);
				else
					writer.WriteNullValue();
				if (v10.Height != null)
					if (v10.Height.Value.IsNumeric)
						writer.WriteNumberValue(v10.Height.Value.NumericValue);
					else
						writer.WriteStringValue(v10.Height.Value.ExpressionValue);
				else
					writer.WriteNullValue();
			}
			else
				throw new NotImplementedException();
			writer.WriteEndArray();
		}
		public override IRDVortex Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
			reader.Read();//read start array
			IRDVortex ReadJson;
			if (objectType == typeof(RDPointNI) || objectType == typeof(RDPointNI?))
				ReadJson = new RDPointNI(reader.GetInt32(), reader.Read() ? reader.GetInt32() : 0);
			else if (objectType == typeof(RDPointN) || objectType == typeof(RDPointN?))
				ReadJson = new RDPointN(reader.GetSingle(), reader.Read() ? reader.GetSingle() : 0);
			else if (objectType == typeof(RDPointI) || objectType == typeof(RDPointI?))
				ReadJson = new RDPointI(
					reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null,
					reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null);
			else if (objectType == typeof(RDPoint) || objectType == typeof(RDPoint?))
				ReadJson = new RDPoint(
					reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null,
					reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null);
			else if (objectType == typeof(RDPointE) || objectType == typeof(RDPointE?))
				ReadJson = new RDPointE(
					reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
					reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
					(RDExpression?)null,
					reader.Read() ?
					reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
					reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
					(RDExpression?)null :
					null
					);
			else if (objectType == typeof(RDSizeNI) || objectType == typeof(RDSizeNI?))
				ReadJson = new RDSizeNI(reader.GetInt32(), reader.Read() ? reader.GetInt32() : 0);
			else if (objectType == typeof(RDSizeN) || objectType == typeof(RDSizeN?))
				ReadJson = new RDSizeN(reader.GetSingle(), reader.Read() ? reader.GetSingle() : 0);
			else if (objectType == typeof(RDSizeI) || objectType == typeof(RDSizeI?))
				ReadJson = new RDSizeI(
					reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null,
					reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null
					);
			else if (objectType == typeof(RDSize) || objectType == typeof(RDSize?))
				ReadJson = new RDSize(
					reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null,
					reader.Read()&&reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null
					);
			else if (objectType == typeof(RDSizeE) || objectType == typeof(RDSizeE?))
				ReadJson = new RDSizeE(
					reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
					reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
					(RDExpression?)null,
					reader.Read() ?
					reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
					reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
					(RDExpression?)null : 
					null
					);
			else
				throw new NotImplementedException();
			if(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
				throw new JsonException("Expected end array token.");
			return ReadJson;
		}
		public override bool CanConvert(Type objectType) => typeof(IRDVortex).IsAssignableFrom(objectType);
	}
}
