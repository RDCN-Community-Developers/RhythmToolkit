using System;

namespace RhythmBase.Events
{

	public class PlayExpression : BaseRowAnimation
	{

		public PlayExpression()
		{
			Type = EventType.PlayExpression;
			Tab = Tabs.Actions;
		}


		public string Expression { get; set; }


		public bool Replace { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public override string ToString() => base.ToString() + string.Format(" {0}", Expression);
	}
}
