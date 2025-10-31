﻿using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents a collection of row events in a level.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RowCollection"/> class.
	/// </remarks>
	/// <param name="parent">The parent <see cref="RDLevel"/> instance associated with this collection.</param>
	public class RowCollection(RDLevel parent) : LevelElementCollection<Row>(parent, true)
	{
		internal readonly List<BaseRowAction> _unhandledRowEvents = [];
		/// <summary>
		/// Adds a <see cref="Row"/> to the collection.
		/// </summary>
		/// <param name="row">The <see cref="Row"/> to add.</param>
		public override void Add(Row row)
		{
			if (_items.Contains(row))
				return;
			row.Parent = parent;
			row.calculator = parent.Calculator;
			foreach (var i in row)
				parent.Add(i);
			foreach(var e in _unhandledRowEvents.Where(i=>i.Index == Count))
				row.Add(e);
			_items.Add(row);
		}
		/// <summary>
		/// Removes a <see cref="Row"/> from the collection.
		/// </summary>
		/// <param name="row">The <see cref="Row"/> to remove.</param>
		/// <returns>True if the item was successfully removed; otherwise, false.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the <paramref name="row"/> is null.</exception>
		public override bool Remove(Row row)
		{
			if (!_items.Contains(row))
				return false;
			BaseRowAction[] rowsToRemove = [.. row];
			foreach (var i in rowsToRemove)
				parent.Remove(i);
			row.Parent = null;
			row.calculator = null;
			return _items.Remove(row);
		}
		/// <summary>  
		/// Gets or sets the <see cref="Row"/> at the specified index.  
		/// </summary>  
		/// <param name="index">The zero-based index of the <see cref="Row"/> to get or set.</param>  
		/// <returns>The <see cref="Row"/> at the specified index.</returns>  
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the specified index is out of range.</exception>  
		public Row this[int index]
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
		/// Gets an enumerable collection of <see cref="Row"/> instances associated with the specified <see cref="RDRoom"/>.  
		/// </summary>  
		/// <param name="room">The <see cref="RDRoom"/> to filter the <see cref="Row"/> instances by.</param>  
		/// <returns>An enumerable collection of <see cref="Row"/> instances associated with the specified room.</returns>  
		public IEnumerable<Row> this[RDRoom room]
		{
			get
			{
				foreach (var item in _items)
				{
					if (room.Contains(item.Rooms))
						yield return item;
				}
			}
		}
		/// <summary>  
		/// Removes the <see cref="Row"/> at the specified index from the collection.  
		/// </summary>  
		/// <param name="index">The zero-based index of the <see cref="Row"/> to remove.</param>  
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the specified index is out of range.</exception>  
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= _items.Count)
				throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
			_items[index].Parent = null;
			_items.RemoveAt(index);
		}
		/// <inheritdoc/>
		public override IEnumerable<Row> ElementsOf(RDRoom room) => _items.Where(item => room.Contains(item.Rooms));
	}
}