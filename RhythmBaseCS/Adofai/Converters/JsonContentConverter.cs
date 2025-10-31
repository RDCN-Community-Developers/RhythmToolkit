using System.Text.Json;
using System.Text.Json.Serialization;
using RhythmBase.Adofai.Converters;
using RhythmBase.Adofai.Utils;

namespace RhythmBase.Adofai.Converters
{
	internal class JsonContentConverter : JsonConverter<JsonElement>
	{
		public override JsonElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var v = FilterTypeUtils.converters;
			return JsonSerializer.Deserialize<JsonElement>($"{{{reader.GetString()}}}", options);
		}

		public override void Write(Utf8JsonWriter writer, JsonElement value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString().TrimStart('{').TrimEnd('}'));
		}
	}
}
