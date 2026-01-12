using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a base action for a row event.
/// </summary>
public abstract class BaseRowAction : BaseEvent
{
	/// <summary>
	/// Gets or sets the parent row event collection.
	/// </summary>
	public Row? Parent
	{
		get => _parent;
		internal set
		{
			if (_parent != null)
			{
				_parent.Remove(this);
				value?.Add(this);
			}
			_parent = value;
		}
	}
	/// <summary>
	/// Gets the room associated with this action.
	/// </summary>
	public RDSingleRoom Room => _parent?.Room ?? RDSingleRoom.Default;
	/// <summary>
	/// Gets the index of the row in the parent collection.
	/// </summary>
	public int Index => Parent?.Index ?? _row;
	///<inheritdoc/>
	public override TEvent Clone<TEvent>()
	{
		TEvent temp = base.Clone<TEvent>();
		if(temp is BaseRowAction tempAction)
			tempAction._parent = Parent;
		return temp;
	}
	/// <summary>
	/// Clones this event and assigns it to a specified row event collection.
	/// </summary>
	internal TEvent Clone<TEvent>(Row row) where TEvent : BaseRowAction, new()
	{
		TEvent temp = base.Clone<TEvent>(row.Parent ?? throw new RhythmBaseException());
		temp.Parent = row;
		return temp;
	}
	internal Row? _parent;
	internal int _row;
}
