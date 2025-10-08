namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that plays an expression.
	/// </summary>
	public class PlayExpression : BaseRowAnimation
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayExpression"/> class.
		/// </summary>
		public PlayExpression() { }
		/// <summary>
		/// Gets or sets the expression to be played.
		/// </summary>
		public string Expression { get; set; } = "neutral";
		/// <summary>
		/// Gets or sets a value indicating whether to replace the current expression.
		/// </summary>
		public bool Replace { get; set; } = false;
		/// <summary>
		/// Gets or sets the target string used for replacement operations.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(Replace)}")]
		public string Target { get; set; } = "neutral";
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.PlayExpression;
		/// <summary>
		/// Gets the tab where the event is categorized.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Expression}";
	}
}
