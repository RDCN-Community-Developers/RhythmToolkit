using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Settings;
namespace RhythmBase.Converters
{
	internal class CustomEventConverter(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<CustomEvent>(level, inputSettings)
	{
		public override CustomEvent? GetDeserializedObject(JObject jobj, Type objectType, CustomEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			CustomEvent? result = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if(result == null) return null;
			result.Data = jobj;
			return result;
		}

		public override JObject SetSerializedObject(CustomEvent value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			JObject data = (JObject)value.Data.DeepClone();
			foreach (KeyValuePair<string, JToken?> item in data)
			{
				jobj[item.Key] = item.Value;
			}
			return jobj;
		}
	}
}
