using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class AnchorStyleConverter : JsonConverter<FloatingText.FloatingTextAnchorStyle>
	{
		public override void WriteJson(JsonWriter writer, FloatingText.FloatingTextAnchorStyle value, JsonSerializer serializer)
		{
			var horizontal = value & (FloatingText.FloatingTextAnchorStyle.Left | FloatingText.FloatingTextAnchorStyle.Right);
			var vertical = value & (FloatingText.FloatingTextAnchorStyle.Upper | FloatingText.FloatingTextAnchorStyle.Lower);
			writer.WriteValue(
				(vertical == 0 ?
					"Middle"
					: vertical.ToString())
				+ horizontal.ToString()
			);
		}
		public override FloatingText.FloatingTextAnchorStyle ReadJson(JsonReader reader, Type objectType, FloatingText.FloatingTextAnchorStyle existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.ReadFrom(reader);
			string JString = token.ToObject<string>() ?? throw new ConvertingException("Cannot read the anchor.");
			Match match = AnchorStyleRegex().Match(JString);
			if (!match.Success)
				throw new ConvertingException(token, $"Illegal Anchor: {JString}");
			FloatingText.FloatingTextAnchorStyle result = FloatingText.FloatingTextAnchorStyle.Center;
			result |= match.Groups[1].Value switch
			{
				"Upper" => FloatingText.FloatingTextAnchorStyle.Upper,
				"Lower" => FloatingText.FloatingTextAnchorStyle.Lower,
				_ => 0,
			};
			result |= match.Groups[2].Value switch
			{
				"Left" => FloatingText.FloatingTextAnchorStyle.Left,
				"Right" => FloatingText.FloatingTextAnchorStyle.Right,
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
