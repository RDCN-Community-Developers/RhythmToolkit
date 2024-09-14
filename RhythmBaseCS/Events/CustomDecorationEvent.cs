using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Exceptions;
using RhythmBase.Extensions;
using RhythmBase.Settings;

namespace RhythmBase.Events
{

	public class CustomDecorationEvent : BaseDecorationAction
	{

		public override EventType Type { get; }


		[JsonIgnore]
		public string ActureType
		{
			get
			{
				return Data["Type".ToLowerCamelCase()].ToString();
			}
		}


		public override Tabs Tab { get; }


		public CustomDecorationEvent()
		{
			Data = [];
			Type = EventType.CustomDecorationEvent;
			Tab = Tabs.Decorations;
			Data = [];
		}


		public CustomDecorationEvent(JObject data)
		{
			Data = [];
			Type = EventType.CustomDecorationEvent;
			Tab = Tabs.Decorations;
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


		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings) => TryConvert(ref value, ref type, settings);


		public static implicit operator CustomEvent(CustomDecorationEvent e) => new CustomEvent(e.Data);


		public static explicit operator CustomDecorationEvent(CustomEvent e)
		{
			bool flag = e.Data["row"] != null;
			if (flag)
			{
				return new CustomDecorationEvent(e.Data);
			}
			throw new RhythmBaseException("The row field is missing from the field contained in this object.");
		}


		public JObject Data;
	}
}
