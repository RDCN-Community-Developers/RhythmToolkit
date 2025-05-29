using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Components.Conditions
{
	/// <summary>
	/// Represents a condition based on the number of times it has been executed.
	/// </summary>
	public class TimesExecutedCondition : BaseConditional
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimesExecutedCondition"/> class.
		/// </summary>
		public TimesExecutedCondition()
		{
			Type = ConditionType.TimesExecuted;
		}
		/// <summary>
		/// Gets or sets the maximum number of executions allowed.
		/// </summary>
		public int MaxTimes { get; set; }
		/// <summary>
		/// Gets the type of the condition.
		/// </summary>
		public override ConditionType Type { get; }
	}
}
