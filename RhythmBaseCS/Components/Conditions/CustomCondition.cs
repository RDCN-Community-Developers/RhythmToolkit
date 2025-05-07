namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Represents a custom condition with an expression.
	/// </summary>
	public class CustomCondition : BaseConditional
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomCondition"/> class.
		/// </summary>
		public CustomCondition()
		{
			Type = ConditionType.Custom;
		}		/// <summary>
		/// Gets or sets the expression for the custom condition.
		/// </summary>
		/// <value>The expression as a string.</value>
		public string Expression { get; set; } = "";		/// <summary>
		/// Gets the type of the condition.
		/// </summary>
		/// <value>The type of the condition, which is <see cref="BaseConditional.ConditionType.Custom"/>.</value>
		public override ConditionType Type { get; }
	}
}
