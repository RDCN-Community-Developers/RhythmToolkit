using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace RhythmBase.Converters
{
	internal class FloatConverter : JsonConverter<float>
	{
		public override bool CanRead => false;	
		public override bool CanWrite => true;
		public override float ReadJson(JsonReader reader, Type objectType, float existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
		public override void WriteJson(JsonWriter writer, float value,  JsonSerializer serializer)
		{
			var formatted = value.ToString();
			writer.WriteRawValue(formatted);
		}
	}
}
