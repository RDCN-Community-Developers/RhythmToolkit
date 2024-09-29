using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RhythmBase.Components;
namespace RhythmBase.Converters
{
	internal class ConditionalConverter : JsonConverter<BaseConditional>
	{
		public override void WriteJson(JsonWriter writer, BaseConditional value, JsonSerializer serializer) => writer.WriteRawValue(JsonConvert.SerializeObject(value, new JsonSerializerSettings
		{
			Converters =
				{
					new StringEnumConverter()
				},
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		}));

		public override BaseConditional ReadJson(JsonReader reader, Type objectType, BaseConditional? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JObject J = JObject.Load(reader);
			Type? SubClassType = Type.GetType($"{typeof(BaseConditional).Namespace}.Conditions.{J["type"]}Condition");
			if (SubClassType == null)
				throw new Exceptions.ConvertingException(J,new Exception($"Unreadable condition: \"{J["type"]}\". path \"{reader.Path}\""));
			return (BaseConditional)J.ToObject(SubClassType)!;
		}
	}
}
