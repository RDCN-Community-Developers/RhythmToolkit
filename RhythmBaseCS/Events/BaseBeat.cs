using System;
namespace RhythmBase.Events
{
	public abstract class BaseBeat : BaseRowAction
	{
		protected BaseBeat()
		{
			Tab = Tabs.Rows;
		}

		public override Tabs Tab { get; }
	}
}
