namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an abstract base class for beat actions in a rhythm-based application.
/// </summary>
public abstract record class BaseBeat : BaseRowAction
{
	/// <summary>
	/// Gets the tab associated with the beat action, which is always set to Rows.
	/// </summary>
	public override Tab Tab => Tab.Rows;
}
