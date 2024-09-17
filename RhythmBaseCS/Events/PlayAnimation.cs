using System;
using Newtonsoft.Json;
namespace RhythmBase.Events
{
	public class PlayAnimation : BaseDecorationAction
	{
		public PlayAnimation()
		{
			Type = EventType.PlayAnimation;
			Tab = Tabs.Decorations;
		}

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
		public string Expression { get; set; }

		public override string ToString() => base.ToString() + string.Format(" Expression:{0}", Expression);
	}
}
