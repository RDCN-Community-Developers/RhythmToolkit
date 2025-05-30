﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Global.Settings;

namespace RhythmBase.Adofai.Converters
{
	internal class ADBaseTileEventConverter<TEvent>(ADLevel level, LevelReadOrWriteSettings inputSettings) : ADBaseEventConverter<TEvent>(level, inputSettings) where TEvent : BaseTileEvent
	{
		public override TEvent GetDeserializedObject(JObject jobj, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken jtoken = jobj["floor"];
			int? parentIndex = (jtoken != null) ? new int?(jtoken.ToObject<int>()) : null;
			_canread = false;
			if (Utils.Utils.ADConvertToType(jobj["eventType"].ToObject<string>()) == typeof(CustomEvent))
			{
				existingValue = (TEvent)(object)new ADCustomTileEventConverter(level, settings).GetDeserializedObject(jobj, objectType, null, hasExistingValue, serializer);
			}
			else
			{
				jobj.Remove("floor");
				existingValue = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			}
			_canread = true;
			if (parentIndex != null)
			{
				existingValue.Parent = level[parentIndex.Value];
				existingValue.Parent.Add((BaseTileEvent)(object)existingValue);
			}
			return existingValue;
		}
		public override JObject SetSerializedObject(TEvent value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			JToken s = jobj.First;
			s.AddBeforeSelf(new JProperty("floor", level.tileOrder.IndexOf(value.Parent)));
			return jobj;
		}
	}
}
