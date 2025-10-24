using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RDPointArrayConverter<TPoint> : JsonConverter<TPoint[]> where TPoint : IRDVortex
	{
		private static readonly RDPointsConverter converter = new();
		public override TPoint[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartArray)
				throw new JsonException($"Unexpected token parsing CountingSoundCollection. Expected StartArray or Null, got {reader.TokenType}.");
			List<TPoint> points = [];
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndArray)
					return [.. points];
				TPoint p = (TPoint)converter.Read(ref reader, typeof(TPoint), options) ?? throw new JsonException("Null audio in CountingSoundCollection.");
				points.Add(p);
			}
			throw new JsonException("Unexpected end of JSON while reading CountingSoundCollection.");
		}
		public override void Write(Utf8JsonWriter writer, TPoint[] value, JsonSerializerOptions options)
		{
			writer.WriteStartArray();
			foreach (var p in value)
				converter.Write(writer, p, options);
			writer.WriteEndArray();
		}
	}
}
