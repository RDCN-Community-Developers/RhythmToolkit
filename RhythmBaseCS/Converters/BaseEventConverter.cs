using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Settings;

namespace RhythmBase.Converters
{
	internal class BaseEventConverter<TEvent>(RDLevel level, LevelReadOrWriteSettings inputSettings) : JsonConverter<TEvent> where TEvent : BaseEvent
	{
		public override bool CanRead => _canread;
		public override bool CanWrite => _canwrite;
		public override void WriteJson(JsonWriter writer, TEvent value, JsonSerializer serializer)
		{
			serializer.Formatting = Formatting.None;
			writer.WriteRawValue(JsonConvert.SerializeObject(SetSerializedObject(value, serializer)));
			serializer.Formatting = Formatting.Indented;
		}
		public override TEvent ReadJson(JsonReader reader, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer) => GetDeserializedObject((JObject)JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer);
		public virtual TEvent GetDeserializedObject(JObject jobj, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			Type SubClassType = Utils.Utils.ConvertToType(jobj["type"].ToObject<string>());
			if (SubClassType == null)
				if (jobj["target"] != null)
					SubClassType = typeof(CustomDecorationEvent);
				else if (jobj["row"] != null)
					SubClassType = typeof(CustomRowEvent);
				else
					SubClassType = typeof(CustomEvent);
			_canread = false;
			existingValue = (TEvent)jobj.ToObject(SubClassType, serializer);
			_canread = true;
			existingValue._beat = level.Calculator.BeatOf(uint.Parse((string)jobj["bar"]), float.Parse((string)(jobj["beat"] ?? 1)));
			return existingValue;
		}
		public virtual JObject SetSerializedObject(TEvent value, JsonSerializer serializer)
		{
			_canwrite = false;
			JObject JObj = JObject.FromObject(value, serializer);
			_canwrite = true;
			JObj.Remove("type");
			ValueTuple<uint, float> b = value.Beat.BarBeat;
			JToken s = JObj.First;
			s.AddBeforeSelf(new JProperty("bar", b.Item1));
			s.AddBeforeSelf(new JProperty("beat", b.Item2));
			s.AddBeforeSelf(new JProperty("type", value.Type.ToString()));
			return JObj;
		}
		protected readonly RDLevel level = level;
		protected readonly LevelReadOrWriteSettings settings = inputSettings;
		private bool _canread = true;
		private bool _canwrite = true;
	}
}
