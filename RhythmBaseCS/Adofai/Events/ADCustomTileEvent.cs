using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RhythmBase.Adofai.Events
{

	public class ADCustomTileEvent : ADBaseTileEvent
	{

		public ADCustomTileEvent()
		{
			Type = ADEventType.CustomTileEvent;
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


		public override string ToString() => string.Format("{0}({1}): {2}", Parent.Index, Parent.Angle, ActureType);
	}
}
