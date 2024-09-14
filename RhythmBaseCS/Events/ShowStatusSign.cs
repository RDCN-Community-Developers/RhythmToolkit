using System;
using Newtonsoft.Json;

namespace RhythmBase.Events
{

	public class ShowStatusSign : BaseEvent
	{

		public ShowStatusSign()
		{
			UseBeats = true;
			Narrate = true;
			Type = EventType.ShowStatusSign;
			Tab = Tabs.Actions;
		}


		public bool UseBeats { get; set; }


		public bool Narrate { get; set; }


		public string Text { get; set; }


		public float Duration { get; set; }


		[JsonIgnore]
		public TimeSpan TimeDuration
		{
			get
			{
				bool useBeats = UseBeats;
				TimeSpan TimeDuration;
				if (useBeats)
				{
					TimeDuration = TimeSpan.Zero;
				}
				else
				{
					TimeDuration = TimeSpan.FromSeconds((double)Duration);
				}
				return TimeDuration;
			}
			set
			{
				UseBeats = false;
				Duration = (float)value.TotalSeconds;
			}
		}


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
}
