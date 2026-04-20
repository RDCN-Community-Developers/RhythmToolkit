using RhythmBase.Global.Components.RichText;
using RhythmBase.RhythmDoctor.Extensions;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.Global.Converters;

internal static partial class ConverterHub
{
    private class ListConverter<T> : JsonConverter<List<T>>
    {
        public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<T> result = new();
            JsonException.ThrowIfNotMatch(reader, [JsonTokenType.StartArray]);
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    break;
                else
                    result.Add(Read<T>(ref reader, options));
            }
            JsonException.ThrowIfNotMatch(reader, [JsonTokenType.EndArray]);
            return result;
        }
        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (T item in value)
                writer.WriteStringValue(item?.ToString() ?? "");
            writer.WriteEndArray();
        }
    }
    private static class ConverterCache<T>
    {
        public static JsonConverter? Converter;
    }
    static ConverterHub()
    {
        InitializeConverters();
        ConverterCache<List<string>>.Converter = new ListConverter<string>();
        ConverterCache<List<FileReference>>.Converter = new ListConverter<FileReference>();
        ConverterCache<RDLine<RDRichStringStyle>>.Converter = new RichTextConverter<RDRichStringStyle>();
    }

    public static T Read<T>(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        if (ConverterCache<T>.Converter is JsonConverter<T> converter)
            return converter.Read(ref reader, typeof(T), options) ?? default!;
        else
        {
            Console.WriteLine((typeof(T)));
            //value = JsonSerializer.Deserialize<T>(ref reader, options) ?? default!;
            return default!;
        }
    }
    public static void Write<T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (ConverterCache<T>.Converter is JsonConverter<T> converter)
            converter.Write(writer, value, options);
        else
            //JsonSerializer.Serialize(writer, value, options);
            writer.WriteNullValue();
    }
}
