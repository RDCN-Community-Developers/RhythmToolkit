using System;
namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Number of executions.
	/// </summary>
	public class TimesExecutedCondition : BaseConditional
	{
		public TimesExecutedCondition()
		{
			Type = ConditionType.TimesExecuted;
		}
		/// <summary>
		/// Maximum number of executions.
		/// </summary>
		public int MaxTimes { get; set; }

		public override ConditionType Type { get; }
	}
}
