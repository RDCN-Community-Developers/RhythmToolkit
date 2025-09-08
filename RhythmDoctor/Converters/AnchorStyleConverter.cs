using System.Text.Json.Serialization;
using RhythmBase.RhythmDoctor.Events;
using System.Text.RegularExpressions;
using System.Text.Json;
namespace RhythmBase.Converters
{
	internal partial class AnchorStyleConverter : JsonConverter<FloatingTextAnchorStyles>
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
		public override FloatingTextAnchorStyles Read(ref Utf8JsonReader reader, Type objectType,  JsonSerializerOptions serializer)
		{
			string JString = reader.GetString() ?? throw new ConvertingException("Cannot read the anchor.");
			Match match = AnchorStyleRegex().Match(JString);
			if (!match.Success)
				return FloatingTextAnchorStyles.Left | FloatingTextAnchorStyles.Upper;
			FloatingTextAnchorStyles result = FloatingTextAnchorStyles.Center;
			result |= match.Groups[1].Value switch
			{
				"Upper" => FloatingTextAnchorStyles.Upper,
				"Lower" => FloatingTextAnchorStyles.Lower,
				_ => 0,
			};
			result |= match.Groups[2].Value switch
			{
				"Left" => FloatingTextAnchorStyles.Left,
				"Right" => FloatingTextAnchorStyles.Right,
				_ => 0,
			};
			return result;
		}
#if NET7_0_OR_GREATER
		[GeneratedRegex("(Upper|Middle|Lower)(Left|Center|Right)")]
		private static partial Regex AnchorStyleRegex();
#elif NETSTANDARD
		private static Regex AnchorStyleRegex() => new("(Upper|Middle|Lower)(Left|Center|Right)", RegexOptions.Compiled);
#endif
	}
}
