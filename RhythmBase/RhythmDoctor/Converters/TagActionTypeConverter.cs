using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class TagActionTypeConverter : JsonConverter<TagActions>
	{
		public override TagActions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ReadOnlySpan<byte> value = reader.ValueSpan;
			TagActions result = 0;
			switch(value)
			{
				case var v when v.StartsWith("Run"u8):
					result |= TagActions.Run;
					value = value.Slice(3);
					break;
					case var v when v.StartsWith("Enable"u8):
					result |= TagActions.Enable;
					value = value.Slice(6);
					break;
					case var v when v.StartsWith("Disable"u8):
					result |= TagActions.Disable;
					value = value.Slice(7);
					break;
				default:
					throw new ConvertingException("Cannot read the TagAction.");
			}
			switch(value)
			{
				case var v when v.IsEmpty:
					break;
				case var v when v.SequenceEqual("All"u8):
					result |= TagActions.All;
					break;
				default:
					throw new ConvertingException("Cannot read the TagAction.");
			}
			return result;
		}

		public override void Write(Utf8JsonWriter writer, TagActions value, JsonSerializerOptions options)
		{
			bool isAll = value.HasFlag(TagActions.All);
			TagActions action = value & (TagActions.Run | TagActions.Enable | TagActions.Disable);
			writer.WriteStringValue(
				isAll
					? action.ToString() + "All"
					: action.ToString()
				);
		}
	}
}
