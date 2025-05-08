using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RhythmBase.Global.Exceptions;	
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class ConditionalConverter : JsonConverter<BaseConditional>
	{
		public override void WriteJson(JsonWriter writer, BaseConditional? value, JsonSerializer serializer) => writer.WriteRawValue(JsonConvert.SerializeObject(value, new JsonSerializerSettings
		{
			Converters = { new StringEnumConverter() },
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		}));

		public override BaseConditional ReadJson(JsonReader reader, Type objectType, BaseConditional? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JObject J = JObject.Load(reader);
			Type? SubClassType = Type.GetType($"{typeof(BaseConditional).Namespace}.Conditions.{J["type"]}Condition");
			return SubClassType == null
				? throw new ConvertingException(J, new Exception($"Unreadable condition: \"{J["type"]}\". path \"{reader.Path}\""))
				: (BaseConditional)J.ToObject(SubClassType)!;
		}
	}
}
