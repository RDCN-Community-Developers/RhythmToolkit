using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Utils;
using RhythmBase.Settings;

namespace RhythmBase.Adofai.Converters
{

	internal class ADBaseTileEventConverter<TEvent>(ADLevel level, LevelReadOrWriteSettings inputSettings) : ADBaseEventConverter<TEvent>(level, inputSettings) where TEvent : ADBaseTileEvent
	{
		public override TEvent GetDeserializedObject(JObject jobj, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken jtoken = jobj["floor"];
			int? parentIndex = (jtoken != null) ? new int?(jtoken.ToObject<int>()) : null;
			_canread = false;
			bool flag = Utils.Utils.ADConvertToType(jobj["eventType"].ToObject<string>()) == typeof(ADCustomEvent);
			if (flag)
			{
				existingValue = (TEvent)(object)new ADCustomTileEventConverter(level, settings).GetDeserializedObject(jobj, objectType, null, hasExistingValue, serializer);
			}
			else
			{
				jobj.Remove("floor");
				existingValue = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			}
			_canread = true;
			bool flag2 = parentIndex != null;
			if (flag2)
			{
				existingValue.Parent = level[parentIndex.Value];
				existingValue.Parent.Add((ADBaseTileEvent)(object)existingValue);
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
