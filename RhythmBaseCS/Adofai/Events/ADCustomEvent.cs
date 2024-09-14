using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RhythmBase.Adofai.Events
{

	public class ADCustomEvent : ADBaseEvent
	{

		public ADCustomEvent()
		{
			Type = ADEventType.CustomEvent;
		}


		public override ADEventType Type { get; }


		[JsonIgnore]
		public string ActureType
		{
			get
			{
				return Data["eventType"].ToString();
			}
		}


		public JObject Data { get; set; }


		public override string ToString() => ActureType;
	}
}
