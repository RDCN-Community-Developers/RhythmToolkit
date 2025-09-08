using Newtonsoft.Json;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events
{
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
		public RDSingleRoom Room => _parent?.Rooms ?? RDSingleRoom.Default;
		/// <summary>
		/// Gets the index of the row in the parent collection.
		/// </summary>
		public int Index => Parent?.Index ?? _row;
		/// <summary>
		/// Clones this event and its basic properties. Clone will be added to the level.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		public override TEvent Clone<TEvent>()
		{
			TEvent Temp = base.Clone<TEvent>();
			if(Temp is BaseRowAction TempAction)
				TempAction._parent = Parent;
			return Temp;
		}
		/// <summary>
		/// Clones this event and assigns it to a specified row event collection.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <param name="row">The row event collection to assign the clone to.</param>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		internal TEvent Clone<TEvent>(Row row) where TEvent : BaseRowAction, new()
		{
			TEvent Temp = base.Clone<TEvent>(row.Parent ?? throw new RhythmBaseException());
			Temp.Parent = row;
			return Temp;
		}
		internal Row? _parent;
		internal int _row;
	}
}
