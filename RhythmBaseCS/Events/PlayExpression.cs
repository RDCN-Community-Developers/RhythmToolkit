namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event that plays an expression.
	/// </summary>
	public class PlayExpression : BaseRowAnimation
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayExpression"/> class.
		/// </summary>
		public PlayExpression()
		{
			Type = EventType.PlayExpression;
			Tab = Tabs.Actions;
		}		/// <summary>
		/// Gets or sets the expression to be played.
		/// </summary>
		public string Expression { get; set; } = "";		/// <summary>
		/// Gets or sets a value indicating whether to replace the current expression.
		/// </summary>
		public bool Replace { get; set; }		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }		/// <summary>
		/// Gets the tab where the event is categorized.
		/// </summary>
		public override Tabs Tab { get; }		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + $" {Expression}";
	}
}
