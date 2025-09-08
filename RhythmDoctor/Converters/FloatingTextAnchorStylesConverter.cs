using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class FloatingTextAnchorStylesConverter : JsonConverter<FloatingTextAnchorStyles>
	{
		public override FloatingTextAnchorStyles Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.String)
				throw new JsonException($"Expected String token, but got {reader.TokenType}.");
			FloatingTextAnchorStyles value = reader.ValueSpan switch
			{
				var s when s.SequenceEqual("UpperLeft"u8) => FloatingTextAnchorStyles.Upper | FloatingTextAnchorStyles.Left,
				var s when s.SequenceEqual("UpperCenter"u8) => FloatingTextAnchorStyles.Upper,
				var s when s.SequenceEqual("UpperRight"u8) => FloatingTextAnchorStyles.Upper | FloatingTextAnchorStyles.Right,
				var s when s.SequenceEqual("MiddleLeft"u8) => FloatingTextAnchorStyles.Left,
				var s when s.SequenceEqual("MiddleCenter"u8) => 0,
				var s when s.SequenceEqual("MiddleRight"u8) => FloatingTextAnchorStyles.Right,
				var s when s.SequenceEqual("LowerLeft"u8) => FloatingTextAnchorStyles.Lower | FloatingTextAnchorStyles.Left,
				var s when s.SequenceEqual("LowerCenter"u8) => FloatingTextAnchorStyles.Lower,
				var s when s.SequenceEqual("LowerRight"u8) => FloatingTextAnchorStyles.Lower | FloatingTextAnchorStyles.Right,
				_ => throw new JsonException($"Unknown FloatingTextAnchorStyles value: {reader.GetString()}"),
			};
			return value;
		}

		public override void Write(Utf8JsonWriter writer, FloatingTextAnchorStyles value, JsonSerializerOptions options)
		{
			ReadOnlySpan<byte> s = value switch
			{
				FloatingTextAnchorStyles.Upper | FloatingTextAnchorStyles.Left => "UpperLeft"u8,
				FloatingTextAnchorStyles.Upper => "UpperCenter"u8,
				FloatingTextAnchorStyles.Upper | FloatingTextAnchorStyles.Right => "UpperRight"u8,
				FloatingTextAnchorStyles.Left => "MiddleLeft"u8,
				0 => "MiddleCenter"u8,
				FloatingTextAnchorStyles.Right => "MiddleRight"u8,
				FloatingTextAnchorStyles.Lower | FloatingTextAnchorStyles.Left => "LowerLeft"u8,
				FloatingTextAnchorStyles.Lower => "LowerCenter"u8,
				FloatingTextAnchorStyles.Lower | FloatingTextAnchorStyles.Right => "LowerRight"u8,
				_ => throw new JsonException($"Unknown FloatingTextAnchorStyles value: {value}"),
			};
			writer.WriteStringValue(s);
		}
	}
}
