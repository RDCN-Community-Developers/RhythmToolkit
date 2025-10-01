using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents a collection of decoration events in a level.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="DecorationCollection"/> class.
	/// </remarks>
	/// <param name="parent">The parent <see cref="RDLevel"/> instance associated with this collection.</param>
	public class DecorationCollection(RDLevel parent) : LevelElementCollection<Decoration>(parent, false)
	{
		internal readonly List<BaseDecorationAction> _unhandledRowEvents = [];
		/// <summary>
		/// Adds a <see cref="Decoration"/> to the collection.
		/// </summary>
		/// <param name="decoration">The <see cref="Decoration"/> to add.</param>
		public override void Add(Decoration decoration)
		{
			if (_items.Contains(decoration))
				return;
			decoration.Parent = parent;
			decoration.calculator = parent.Calculator;
			foreach (BaseDecorationAction i in decoration)
				parent.AddInternal(i);
			_items.Add(decoration);
		}
		/// <summary>
		/// Removes a <see cref="Decoration"/> from the collection.
		/// </summary>
		/// <param name="item">The <see cref="Decoration"/> to remove.</param>
		/// <returns>True if the item was successfully removed; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the <paramref name="item"/> is null.</exception>
		public override bool Remove(Decoration decoration)
		{
			if (!_items.Contains(decoration))
				return false;
			BaseDecorationAction[] decosToRemove = [.. decoration];
			foreach (var i in decosToRemove)
				parent.Remove(i);
			decoration.Parent = null;
			decoration.calculator = null;
			return _items.Remove(decoration);
		}
		/// <summary>
		/// Gets or sets the <see cref="Decoration"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the <see cref="Decoration"/> to get or set.</param>
		/// <returns>The <see cref="Decoration"/> at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of range.</exception>
		public Decoration this[int index]
		{
			get => _items[index];
			set
			{
				if (index < 0 || index >= _items.Count)
					throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
				_items[index].Parent = null;
				value.Parent = parent;
				_items[index] = value;
			}
		}
		/// <summary>
		/// Gets an enumerable collection of <see cref="Decoration"/> associated with the specified <see cref="RDRoom"/>.
		/// </summary>
		/// <param name="room">The <see cref="RDRoom"/> to filter the decorations by.</param>
		/// <returns>An enumerable collection of <see cref="Decoration"/> in the specified room.</returns>
		public IEnumerable<Decoration> this[RDRoom room]
		{
			get
			{
				foreach (var item in _items)
				{
					if (room.Contains(item.Room))
						yield return item;
				}
			}
		}
		/// <summary>
		/// Gets the <see cref="Decoration"/> with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the <see cref="Decoration"/> to retrieve.</param>
		/// <returns>The <see cref="Decoration"/> with the specified ID, or null if not found.</returns>
		public Decoration? this[string id]
		{
			get
			{
				foreach (var item in _items)
				{
					if (item.Id == id)
						return item;
				}
				return null;
			}
		}
		/// <summary>  
		/// Removes the <see cref="Decoration"/> at the specified index.  
		/// </summary>  
		/// <param name="index">The zero-based index of the <see cref="Decoration"/> to remove.</param>  
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of range.</exception>  
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= _items.Count)
				throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
			_items[index].Parent = null;
			_items.RemoveAt(index);
		}
		/// <inheritdoc/>
		public override IEnumerable<Decoration> ElementsOf(RDRoom room) => _items.Where(item => room.Contains(item.Room));
	}
}
