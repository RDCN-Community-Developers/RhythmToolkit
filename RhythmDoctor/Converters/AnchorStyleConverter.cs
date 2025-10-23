using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal class AnchorStyleConverter : JsonConverter<FloatingTextAnchorStyles>
	{
		public override void Write(Utf8JsonWriter writer, FloatingTextAnchorStyles value, JsonSerializerOptions serializer)
		{
			var horizontal = value & (FloatingTextAnchorStyles.Left | FloatingTextAnchorStyles.Right);
			var vertical = value & (FloatingTextAnchorStyles.Upper | FloatingTextAnchorStyles.Lower);
			writer.WriteStringValue(
				(vertical == 0 ?
					"Middle"
					: vertical.ToString())
				+ horizontal.ToString()
			);
		}
		public override FloatingTextAnchorStyles Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
		{
			ReadOnlySpan<byte> value = reader.ValueSpan;
			FloatingTextAnchorStyles result = FloatingTextAnchorStyles.Center;
			switch (value)
			{
				case var v when v.StartsWith("Upper"u8):
					result |= FloatingTextAnchorStyles.Upper;
					value = value.Slice(5);
					break;
				case var v when v.StartsWith("Lower"u8):
					result |= FloatingTextAnchorStyles.Lower;
					value = value.Slice(5);
					break;
				case var v when v.StartsWith("Middle"u8):
					value = value.Slice(6);
					break;
				default:
					throw new ConvertingException("Cannot read the anchor.");
			}
			switch (value)
			{
				case var v when v.SequenceEqual("Left"u8):
					result |= FloatingTextAnchorStyles.Left;
					break;
				case var v when v.SequenceEqual("Right"u8):
					result |= FloatingTextAnchorStyles.Right;
					break;
				case var v when v.SequenceEqual("Center"u8):
					break;
				default:
					throw new ConvertingException("Cannot read the anchor.");
			}
			return result;
		}
	}
}
