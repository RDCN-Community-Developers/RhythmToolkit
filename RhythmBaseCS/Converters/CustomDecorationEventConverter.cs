using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Settings;
namespace RhythmBase.Converters
{
	internal class CustomDecorationEventConverter(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseDecorationActionConverter<CustomDecorationEvent>(level, inputSettings)
	{
		public override CustomDecorationEvent GetDeserializedObject(JObject jobj, Type objectType, CustomDecorationEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			CustomDecorationEvent result = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			result.Data = jobj;
			return result;
		}

		public override JObject SetSerializedObject(CustomDecorationEvent value, JsonSerializer serializer)
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
