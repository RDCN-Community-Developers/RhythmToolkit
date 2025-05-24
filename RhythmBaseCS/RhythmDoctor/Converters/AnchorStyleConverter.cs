using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class AnchorStyleConverter : JsonConverter<FloatingText.AnchorStyle>
	{
		public override void WriteJson(JsonWriter writer, FloatingText.AnchorStyle value, JsonSerializer serializer)
		{
			var horizontal = value & (FloatingText.AnchorStyle.Left | FloatingText.AnchorStyle.Right);
			var vertical = value & (FloatingText.AnchorStyle.Upper | FloatingText.AnchorStyle.Lower);
			writer.WriteValue(
				(vertical == 0 ?
					"Middle"
					: vertical.ToString())
				+ horizontal.ToString()
			);
		}
		public override FloatingText.AnchorStyle ReadJson(JsonReader reader, Type objectType, FloatingText.AnchorStyle existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.ReadFrom(reader);
			string JString = token.ToObject<string>() ?? throw new ConvertingException("Cannot read the anchor.");
			Match match = AnchorStyleRegex().Match(JString);
			if (!match.Success)
				throw new ConvertingException(token, $"Illegal Anchor: {JString}");
			FloatingText.AnchorStyle result = FloatingText.AnchorStyle.Center;
			result |= match.Groups[1].Value switch
			{
				"Upper" => FloatingText.AnchorStyle.Upper,
				"Lower" => FloatingText.AnchorStyle.Lower,
				_ => 0,
			};
			result |= match.Groups[2].Value switch
			{
				"Left" => FloatingText.AnchorStyle.Left,
				"Right" => FloatingText.AnchorStyle.Right,
				_ => 0,
			};
			return result;
		}
#if NET7_0_OR_GREATER
		[GeneratedRegex("(Upper|Middle|Lower)(Left|Center|Right)")]
		private static partial Regex AnchorStyleRegex();
#elif NETSTANDARD2_1
		private static Regex AnchorStyleRegex() => new("(Upper|Middle|Lower)(Left|Center|Right)", RegexOptions.Compiled);
#endif
	}
}
