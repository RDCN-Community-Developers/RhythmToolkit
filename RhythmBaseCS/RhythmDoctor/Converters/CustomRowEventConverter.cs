using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class CustomRowEventConverter(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseRowActionConverter<CustomRowEvent>(level, inputSettings)
	{
		public override CustomRowEvent? GetDeserializedObject(JObject jobj, Type objectType, CustomRowEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			CustomRowEvent? result = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if (result == null) return null;
			result.Data = jobj;
			return result;
		}

		public override JObject SetSerializedObject(CustomRowEvent value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			JToken data = value.Data.DeepClone();
			foreach (KeyValuePair<string, JToken?> item in jobj)
			{
				data[item.Key] = item.Value;
			}
			return (JObject)data;
		}
	}
}
