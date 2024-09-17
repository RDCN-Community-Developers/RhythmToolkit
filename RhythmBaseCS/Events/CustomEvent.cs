using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Extensions;
using RhythmBase.Settings;
using RhythmBase.Utils;
namespace RhythmBase.Events
{
	public class CustomEvent : BaseEvent
	{
		/// <inheritdoc/>
		[JsonIgnore]
		public override EventType Type => EventType.CustomEvent;
		[JsonIgnore]
		public string ActureType => Data["Type".ToLowerCamelCase()].ToString();
		/// <inheritdoc/>
		public override Tabs Tab { get; }
		/// <inheritdoc/>
		public override int Y
		{
			get => (int)(Data["Y".ToLowerCamelCase()] ?? 0);
			set => Data["Y".ToLowerCamelCase()] = value;
		}
		public CustomEvent()
		{
			Data = [];
			Tab = Tabs.Unknown;
		}
		public CustomEvent(JObject data)
		{
			Data = [];
			Tab = Tabs.Unknown;
			Data = data;
			uint bar = data["bar"].ToObject<uint>();
			JToken jtoken = data["beat"];
			Beat = new Beat(bar, (jtoken != null) ? jtoken.ToObject<float>() : 1f);
			JToken jtoken2 = data["tag"];
			Tag = (jtoken2 != null) ? jtoken2.ToObject<string>() : null;
			Condition condition;
			if (data["condition"] != null)
			{
				JToken jtoken3 = data["condition"];
				condition = Condition.Load((jtoken3 != null) ? jtoken3.ToObject<string>() : null);
			}
			else
			{
				condition = null;
			}
			Condition = condition;
			JToken jtoken4 = data["active"];
			Active = jtoken4 == null || jtoken4.ToObject<bool>();
		}
		/// <inheritdoc/>
		public override string ToString() => string.Format("{0} *{1}", Beat, ActureType);
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings)
		{
			JsonSerializer serializer = JsonSerializer.Create(_beat.BaseLevel.GetSerializer(settings));
			Type eventType = Utils.Utils.ConvertToType(Data["type"]?.ToObject<string>() ?? "");
			bool TryConvert;
			if (eventType == null)
			{
				BaseEvent TempEvent;
				if (Data["target"] != null)
					TempEvent = Data.ToObject<CustomDecorationEvent>(serializer);
				else if (Data["row"] != null)
					TempEvent = Data.ToObject<CustomRowEvent>(serializer);
				else
					TempEvent = Data.ToObject<CustomEvent>(serializer);
				value = TempEvent;
				type = null;
				TryConvert = false;
			}
			else
			{
				BaseEvent TempEvent2 = (BaseEvent)Data.ToObject(eventType, serializer);
				value = TempEvent2;
				type = new EventType?(Enum.Parse<EventType>((string)Data["type"]));
				TryConvert = true;
			}
			return TryConvert;
		}

		[JsonIgnore]
		public JObject Data { get; set; }
	}
}
