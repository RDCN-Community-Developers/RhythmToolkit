using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a base action for a row event.
/// </summary>
public abstract record class BaseRowAction : BaseEvent
{
	/// <summary>
	/// Gets or sets the parent row event collection.
	/// </summary>
	public Row? Parent => _parent;
	/// <summary>
	/// Gets the room associated with this action.
	/// </summary>
	public RDSingleRoom Room => _parent?.Room ?? RDSingleRoom.Default;
	/// <summary>
	/// Gets the index of the row in the parent collection.
	/// </summary>
	public int Index => Parent?.Index ?? _row;
	///<inheritdoc/>
	public override TEvent CloneAs<TEvent>()
	{
		TEvent temp = base.CloneAs<TEvent>();
		if (temp is BaseRowAction tempAction)
			tempAction._row = Parent?.Index ?? -1;
		return temp;
	}
	internal Row? _parent;
	internal int _row;
}
