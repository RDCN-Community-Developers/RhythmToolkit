using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.Global.Converters;

internal abstract class RDPointsConverter<T> : JsonConverter<T> where T : struct, IRDVector
{
    public override bool CanConvert(Type objectType) => typeof(IRDVector).IsAssignableFrom(objectType);
}
[RDJsonConverterFor(typeof(RDPoint))]
internal class RDPointConverter : JsonConverter<RDPoint>
{
    public override RDPoint Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDPoint(
                reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null,
                reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDPoint value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        if (value.X is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.X.Value);
        if (value.Y is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Y.Value);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDPointNI))]
internal class RDPointNIConverter : JsonConverter<RDPointNI>
{
    public override RDPointNI Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDPointNI(
            reader.Read() ? reader.GetInt32() : 0,
            reader.Read() ? reader.GetInt32() : 0);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDPointNI value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.X);
        writer.WriteNumberValue(value.Y);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDPointN))]
internal class RDPointNConverter : JsonConverter<RDPointN>
{
    public override RDPointN Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDPointN(
            reader.Read() ? reader.GetSingle() : 0,
            reader.Read() ? reader.GetSingle() : 0);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDPointN value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.X);
        writer.WriteNumberValue(value.Y);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDPointI))]
internal class RDPointIConverter : JsonConverter<RDPointI>
{
    public override RDPointI Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDPointI(
            reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null,
            reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDPointI value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        if (value.X is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.X.Value);
        if (value.Y is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Y.Value);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDPointE))]
internal class RDPointEConverter : JsonConverter<RDPointE>
{
    public override RDPointE Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDPointE(
            reader.Read() ?
            reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
            reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
            (RDExpression?)null :
            null,
            reader.Read() ?
            reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
            reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
            (RDExpression?)null :
            null
            );
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDPointE value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        if (value.X != null)
            if (value.X.Value.IsNumeric)
                writer.WriteNumberValue(value.X.Value.NumericValue);
            else
                writer.WriteStringValue(value.X.Value.ExpressionValue);
        else
            writer.WriteNullValue();
        if (value.Y != null)
            if (value.Y.Value.IsNumeric)
                writer.WriteNumberValue(value.Y.Value.NumericValue);
            else
                writer.WriteStringValue(value.Y.Value.ExpressionValue);
        else
            writer.WriteNullValue();
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDSizeNI))]
internal class RDSizeNIConverter : JsonConverter<RDSizeNI>
{
    public override RDSizeNI Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDSizeNI(
            reader.Read() ? reader.GetInt32() : 0,
            reader.Read() ? reader.GetInt32() : 0);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDSizeNI value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Width);
        writer.WriteNumberValue(value.Height);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDSizeN))]
internal class RDSizeNConverter : JsonConverter<RDSizeN>
{
    public override RDSizeN Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDSizeN(
            reader.Read() ? reader.GetSingle() : 0,
            reader.Read() ? reader.GetSingle() : 0);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDSizeN value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Width);
        writer.WriteNumberValue(value.Height);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDSizeI))]
internal class RDSizeIConverter : JsonConverter<RDSizeI>
{
    public override RDSizeI Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDSizeI(
            reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null,
            reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetInt32() : null);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDSizeI value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        if (value.Width is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Width.Value);
        if (value.Height is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Height.Value);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDSize))]
internal class RDSizeConverter : JsonConverter<RDSize>
{
    public override RDSize Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDSize(
            reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null,
            reader.Read() && reader.TokenType == JsonTokenType.Number ? reader.GetSingle() : null);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDSize value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        if (value.Width is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Width.Value);
        if (value.Height is null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Height.Value);
        writer.WriteEndArray();
    }
}
[RDJsonConverterFor(typeof(RDSizeE))]
internal class RDSizeEConverter : JsonConverter<RDSizeE>
{
    public override RDSizeE Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        var value = new RDSizeE(
            reader.Read() ?
            reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
            reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
            (RDExpression?)null :
            null,
            reader.Read() ?
            reader.TokenType == JsonTokenType.Number ? new RDExpression(reader.GetSingle()) :
            reader.TokenType == JsonTokenType.String ? new RDExpression(reader.GetString() ?? string.Empty) :
            (RDExpression?)null :
            null);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return value;
    }
    public override void Write(Utf8JsonWriter writer, RDSizeE value, JsonSerializerOptions serializer)
    {
        writer.WriteStartArray();
        if (value.Width != null)
            if (value.Width.Value.IsNumeric)
                writer.WriteNumberValue(value.Width.Value.NumericValue);
            else
                writer.WriteStringValue(value.Width.Value.ExpressionValue);
        else
            writer.WriteNullValue();
        if (value.Height != null)
            if (value.Height.Value.IsNumeric)
                writer.WriteNumberValue(value.Height.Value.NumericValue);
            else
                writer.WriteStringValue(value.Height.Value.ExpressionValue);
        else
            writer.WriteNullValue();
        writer.WriteEndArray();
    }
}