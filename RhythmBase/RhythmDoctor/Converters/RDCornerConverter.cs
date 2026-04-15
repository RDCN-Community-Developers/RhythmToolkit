using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters;

[global::RhythmBase.Global.Converters.RDJsonConverterFor(typeof(Corner))]
internal class RDCornerConverter : JsonConverter<Corner>
{
    public override Corner Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            JsonException.Throw(reader, ["StartArray", "Null"]);
        reader.Read();
        Corner corner = new()
        {
            LeftBottom = ReadOneCorner(ref reader),
            RightBottom = ReadOneCorner(ref reader),
            LeftTop = ReadOneCorner(ref reader),
            RightTop = ReadOneCorner(ref reader)
        };

        if (reader.TokenType != JsonTokenType.EndArray)
            JsonException.Throw(reader, ["EndArray"]);
        return corner;
    }
    private static RDPoint? ReadOneCorner(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            reader.Read();
            return null;
        }
        else if (reader.TokenType == JsonTokenType.StartArray)
        {
            reader.Read();
            RDPoint p = new();
            if (reader.TokenType == JsonTokenType.Null)
                p.X = null;
            else if (reader.TokenType == JsonTokenType.Number)
                p.X = reader.GetSingle();
            else
                throw JsonException.Throw(reader, ["Number", "Null"]);
            reader.Read();
            if (reader.TokenType == JsonTokenType.Null)
                p.Y = null;
            else if (reader.TokenType == JsonTokenType.Number)
                p.Y = reader.GetSingle();
            else
                throw JsonException.Throw(reader, ["Number", "Null"]);
            reader.Read();
            if (reader.TokenType != JsonTokenType.EndArray)
                throw JsonException.Throw(reader, ["EndArray"]);
            reader.Read();
            return p;
        }
        else
            throw JsonException.Throw(reader, ["StartArray", "Null"]);
    }
    public override void Write(Utf8JsonWriter writer, Corner value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        WriteOneCorner(writer, value.LeftBottom);
        WriteOneCorner(writer, value.RightBottom);
        WriteOneCorner(writer, value.LeftTop);
        WriteOneCorner(writer, value.RightTop);
        writer.WriteEndArray();
    }
    private static void WriteOneCorner(Utf8JsonWriter writer, RDPoint? point)
    {
        if (point is RDPoint p)
        {
            writer.WriteStartArray();
            if (p.X is float px)
                writer.WriteNumberValue(px);
            else
                writer.WriteNullValue();
            if (p.Y is float py)
                writer.WriteNumberValue(py);
            else
                writer.WriteNullValue();
            writer.WriteEndArray();
        }
        else
            writer.WriteNullValue();
    }
}