﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class BaseEventConverter<TEvent>(RDLevel? level, LevelReadOrWriteSettings inputSettings) : JsonConverter<TEvent> where TEvent : IBaseEvent
	{
		public override bool CanRead => _canread;
		public override bool CanWrite => _canwrite;
		public BaseEventConverter(LevelReadOrWriteSettings inputSettings) : this(null, inputSettings) { }
		public override void WriteJson(JsonWriter writer, TEvent? value, JsonSerializer serializer)
		{
			if (value == null)
				throw new ConvertingException($"Event is null");
			if (_canwrite)
			{
				_canwrite = false;
				serializer.Formatting = Formatting.None;
				writer.WriteRawValue(JsonConvert.SerializeObject(SetSerializedObject(value, serializer)));
				serializer.Formatting = Formatting.Indented;
				_canwrite = true;
			}
		}
		public override TEvent? ReadJson(JsonReader reader, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer) => GetDeserializedObject((JObject)JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer);
		public virtual TEvent? GetDeserializedObject(JObject jobj, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken? typeToken = jobj["type"]
				?? throw new ConvertingException(jobj, new Exception($"Missing property \"{jobj["type"]}\". path \"{jobj.Path}\""));
			Type SubClassType = EventTypeUtils.ToType(typeToken.ToObject<string>()
				?? throw new ConvertingException(jobj, new Exception($"Missing property \"{typeToken}\". path \"{jobj.Path}\"")));
			if (SubClassType == null)
				if (jobj["target"] != null)
					SubClassType = typeof(CustomDecorationEvent);
				else if (jobj["row"] != null)
					SubClassType = typeof(CustomRowEvent);
				else
					SubClassType = typeof(CustomEvent);
			_canread = false;
			existingValue = (TEvent?)jobj.ToObject(SubClassType, serializer)
				?? throw new ConvertingException(jobj, new Exception($"Cannot convert this event: \"{jobj}\". path \"{jobj.Path}\""));
			_canread = true;
			int bar = int.Parse((string?)jobj["bar"]
				?? throw new Exception($"Missing property \"{jobj["bar"]}\".path \"{jobj.Path}\""));
			float beat = float.Parse((string?)jobj["beat"] ?? 1.ToString());
			RDBeat rdbeat = level?.Calculator.BeatOf(bar, beat) ?? new(bar, beat);
			((BaseEvent)(object)existingValue)._beat = rdbeat;
			return existingValue;
		}
		public virtual JObject SetSerializedObject(TEvent value, JsonSerializer serializer)
		{
			_canwrite = false;
			JObject JObj = JObject.FromObject(value, serializer);
			_canwrite = true;
			JObj.Remove("type");
			(int, float)b = value.Beat.BarBeat;
			JToken s = JObj.First ?? throw new ConvertingException($"Internal error: Missing properties. path \"{JObj.Path}\"");
			s.AddBeforeSelf(new JProperty("bar", b.Item1));
			if (value is not IBarBeginningEvent)
				s.AddBeforeSelf(new JProperty("beat", b.Item2 % 1 == 0 ? (int)b.Item2 : (object)b.Item2));
			s.AddBeforeSelf(new JProperty("type", value.Type.ToString()));
			if (JObj.Value<string>("tag") is string str && string.IsNullOrEmpty(str))
				JObj.Property("tag")?.Remove();
			return JObj;
		}
		protected readonly RDLevel? level = level;
		protected readonly LevelReadOrWriteSettings settings = inputSettings;
		private bool _canread = true;
		private bool _canwrite = true;
	}
}
