﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Utils;
using RhythmBase.Global.Settings;

namespace RhythmBase.Adofai.Converters
{
	internal class ADBaseEventConverter<TEvent>(ADLevel level, LevelReadOrWriteSettings inputSettings) : JsonConverter<TEvent> where TEvent : BaseEvent
	{
		public override bool CanRead
		{
			get
			{
				return _canread;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return _canwrite;
			}
		}
		public override void WriteJson(JsonWriter writer, TEvent? value, JsonSerializer serializer) => throw new NotImplementedException();

		public override TEvent ReadJson(JsonReader reader, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer) => GetDeserializedObject((JObject)JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer);

		public virtual TEvent GetDeserializedObject(JObject jobj, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			Type SubClassType = Utils.Utils.ADConvertToType(jobj["eventType"].ToObject<string>());
			_canread = false;
			existingValue = (TEvent)((SubClassType != null) ? jobj.ToObject(SubClassType, serializer) : jobj.ToObject<CustomEvent>(serializer));
			_canread = true;
			return existingValue;
		}

		public virtual JObject SetSerializedObject(TEvent value, JsonSerializer serializer)
		{
			_canwrite = false;
			JObject JObj = JObject.FromObject(value, serializer);
			_canwrite = true;
			JObj.Remove("type");
			JToken s = JObj.First;
			s.AddBeforeSelf(new JProperty("eventType", value.Type.ToString()));
			return JObj;
		}

		protected readonly ADLevel level = level;

		protected readonly LevelReadOrWriteSettings settings = inputSettings;

		protected bool _canread = true;

		protected bool _canwrite = true;
	}
}
