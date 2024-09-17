using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Extensions;
namespace RhythmBase.Events
{
	public abstract class BaseBeatsPerMinute : BaseEvent
	{
		protected BaseBeatsPerMinute()
		{
			_bpm = 100f;
		}

		public override Beat Beat
		{
			get
			{
				return base.Beat;
			}
			set
			{
				base.Beat = value;
				ResetTimeLine();
			}
		}

		[JsonProperty("bpm")]
		public float BeatsPerMinute
		{
			get
			{
				return _bpm;
			}
			set
			{
				_bpm = value;
				ResetTimeLine();
			}
		}

		private void ResetTimeLine()
		{
			if (Beat.BaseLevel != null)
			{
				foreach (BaseEvent item in from i in Beat.BaseLevel
										   where i.Beat > Beat
										   select i)
				{
					item.Beat.ResetBPM();
				}
			}
		}

		private float _bpm;
	}
}
