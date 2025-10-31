namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an action to set the visibility of a decoration.
	/// </summary>
	public class SetVisible : BaseDecorationAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetVisible"/> class.
		/// </summary>
		public SetVisible() { }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.SetVisible;
		/// <summary>
		/// Gets the tab to which the event belongs.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Decorations;
		/// <summary>
		/// Gets or sets a value indicating whether the decoration is visible.
		/// </summary>
		public bool Visible { get; set; } = true;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Visible);
	}
}
