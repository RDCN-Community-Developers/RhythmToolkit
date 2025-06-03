using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class AnchorStyleConverter : JsonConverter<FloatingTextAnchorStyles>
	{
		public override void WriteJson(JsonWriter writer, FloatingTextAnchorStyles value, JsonSerializer serializer)
		{
			var horizontal = value & (FloatingTextAnchorStyles.Left | FloatingTextAnchorStyles.Right);
			var vertical = value & (FloatingTextAnchorStyles.Upper | FloatingTextAnchorStyles.Lower);
			writer.WriteValue(
				(vertical == 0 ?
					"Middle"
					: vertical.ToString())
				+ horizontal.ToString()
			);
		}
		public override FloatingTextAnchorStyles ReadJson(JsonReader reader, Type objectType, FloatingTextAnchorStyles existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.ReadFrom(reader);
			string JString = token.ToObject<string>() ?? throw new ConvertingException("Cannot read the anchor.");
			Match match = AnchorStyleRegex().Match(JString);
			if (!match.Success)
				throw new ConvertingException(token, $"Illegal Anchor: {JString}");
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
