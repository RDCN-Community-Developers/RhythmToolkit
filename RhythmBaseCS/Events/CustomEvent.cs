using System;
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
		[JsonIgnore]
		public override EventType Type { get; }
		[JsonIgnore]
		public string ActureType => Data["Type".ToLowerCamelCase()].ToString();
		public override Tabs Tab { get; }
		public override int Y
		{
			get => (int)(Data["Y".ToLowerCamelCase()] ?? 0);
			set => Data["Y".ToLowerCamelCase()] = value;
		}
		public CustomEvent()
		{
			Data = [];
			Type = EventType.CustomEvent;
			Tab = Tabs.Unknown;
			Data = [];
		}
		public CustomEvent(JObject data)
		{
			Data = [];
			Type = EventType.CustomEvent;
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
		public override string ToString() => string.Format("{0} *{1}", Beat, ActureType);
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings)
		{
			JsonSerializer serializer = JsonSerializer.Create(_beat.BaseLevel.GetSerializer(settings));
			Type eventType = Utils.Utils.ConvertToType((string)Data["type"]);
			bool flag = eventType == null;
			bool TryConvert;
			if (flag)
			{
				bool flag2 = Data["target"] != null;
				BaseEvent TempEvent;
				if (flag2)
				{
					TempEvent = Data.ToObject<CustomDecorationEvent>(serializer);
				}
				else
				{
					bool flag3 = Data["row"] != null;
					if (flag3)
					{
						TempEvent = Data.ToObject<CustomRowEvent>(serializer);
					}
					else
					{
						TempEvent = Data.ToObject<CustomEvent>(serializer);
					}
				}
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
		public JObject Data;
	}
}
