﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Settings;
namespace RhythmBase.Converters
{
	internal class CustomRowEventConverter(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseRowActionConverter<CustomRowEvent>(level, inputSettings)
	{
		public override CustomRowEvent GetDeserializedObject(JObject jobj, Type objectType, CustomRowEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			CustomRowEvent result = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
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