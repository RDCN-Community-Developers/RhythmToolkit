using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Components;
using System.Text.RegularExpressions;
namespace RhythmBase.Global.Converters
{
	internal class ColorConverter : JsonConverter<RDColor>
	{
		public override void WriteJson(JsonWriter writer, RDColor value, JsonSerializer serializer) => writer.WriteValue(value.ToString("rrggbbaa"));

		public override RDColor ReadJson(JsonReader reader, Type objectType, RDColor existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string JString = JToken.Load(reader).Value<string>() ?? throw new Exceptions.ConvertingException("Cannot read the color.");
			return RDColor.FromRgba(JString);
		}
	}
}
