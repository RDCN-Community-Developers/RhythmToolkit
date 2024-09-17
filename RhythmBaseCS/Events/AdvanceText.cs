using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class AdvanceText : BaseEvent, IRoomEvent
	{
		public AdvanceText()
		{
			Type = EventType.AdvanceText;
			Tab = Tabs.Actions;
		}

		public override EventType Type { get; }

		[JsonIgnore]
		public Room Rooms
		{
			get
			{
				return Parent.Rooms;
			}
			set
			{
				Parent.Rooms = value;
			}
		}

		public override Tabs Tab { get; }

		[JsonIgnore]
		public FloatingText Parent { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float FadeOutDuration { get; set; }

		[JsonProperty]
		private int Id
		{
			get
			{
				return Parent.Id;
			}
		}

		public override string ToString() => base.ToString() + string.Format(" Index:{0}", this.Parent.Children.IndexOf(this));
	}
}
