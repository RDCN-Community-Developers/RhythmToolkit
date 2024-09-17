using System;
namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Expression condition.
	/// </summary>
	public class CustomCondition : BaseConditional
	{
		public CustomCondition()
		{
			Type = ConditionType.Custom;
		}
		/// <summary>
		/// Expression.
		/// </summary>
		/// <returns></returns>
		public string Expression { get; set; }

		public override ConditionType Type { get; }
	}
}
