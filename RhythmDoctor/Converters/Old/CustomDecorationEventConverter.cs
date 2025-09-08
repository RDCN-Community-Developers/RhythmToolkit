using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Converters.Old
{
	internal class CustomDecorationEventConverter(RDLevel? level, LevelReadOrWriteSettings inputSettings) : BaseDecorationActionConverter<CustomDecorationEvent>(level, inputSettings)
	{
		public CustomDecorationEventConverter(LevelReadOrWriteSettings inputSettings) : this(null, inputSettings) { }
		public override CustomDecorationEvent? GetDeserializedObject(JObject jobj, Type objectType, CustomDecorationEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			CustomDecorationEvent? result = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if (result != null)
				result.ExtraData = jobj;
			return result;
		}
		public override JObject SetSerializedObject(CustomDecorationEvent value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			JToken data = value.ExtraData.DeepClone();
			foreach (KeyValuePair<string, JToken?> item in jobj)
			{
				data[item.Key] = item.Value;
			}
			return (JObject)data;
		}
	}
}
