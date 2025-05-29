namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an abstract base class for beat actions in a rhythm-based application.
	/// </summary>
	public abstract class BaseBeat : BaseRowAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseBeat"/> class and sets the Tab property to Rows.
		/// </summary>
		protected BaseBeat() { }
		/// <summary>
		/// Gets the tab associated with the beat action, which is always set to Rows.
		/// </summary>
		public override Tabs Tab => Tabs.Rows;
	}
}
