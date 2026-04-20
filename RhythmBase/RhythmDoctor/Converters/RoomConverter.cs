using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters;

[global::RhythmBase.Global.Converters.RDJsonConverterFor(typeof(RDRoom))]
internal class RoomConverter : JsonConverter<RDRoom>
{
    private byte[] buffer = new byte[Constants.Constants.RoomCount];
    private int index = 0;
    public override RDRoom Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        while (reader.Read())
            if (reader.TokenType == JsonTokenType.EndArray)
                break;
            else
                buffer[index++] = reader.GetByte();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        RDRoom result = new();
        for (int i = 0; i < index; i++)
            result[buffer[i]] = true;
        index = 0;
        return result;
    }

    public override void Write(Utf8JsonWriter writer, RDRoom value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (byte item in value.Rooms)
            writer.WriteNumberValue(item);
        writer.WriteEndArray();
    }
}
[global::RhythmBase.Global.Converters.RDJsonConverterFor(typeof(RDSingleRoom))]
internal class SingleRoomConverter : JsonConverter<RDSingleRoom>
{
    public override RDSingleRoom Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
        reader.Read();
        byte index = reader.GetByte();
        RDSingleRoom result = new(index);
        reader.Read();
        JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
        return result;
    }

    public override void Write(Utf8JsonWriter writer, RDSingleRoom value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Value);
        writer.WriteEndArray();
    }
}
