using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;

using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal class ColorConverter : JsonConverter<RDColor>
	{
		public override void WriteJson(JsonWriter writer, RDColor value, JsonSerializer serializer) => writer.WriteValue(value.ToString("RRGGBBAA"));

		public override RDColor ReadJson(JsonReader reader, Type objectType, RDColor existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string JString = JToken.Load(reader).Value<string>() ?? throw new RhythmBase.Exceptions.ConvertingException("Cannot read the color.");
			return RDColor.FromRgba(JString);
		}
	}
}
