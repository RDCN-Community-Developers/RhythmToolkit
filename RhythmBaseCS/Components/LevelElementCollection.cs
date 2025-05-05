using System.Collections;

namespace RhythmBase.Components
{
	/// <summary>
	/// Represents a collection used to manage <see cref="Row"/> or <see cref="Decoration"/> elements.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection, constrained to <see cref="OrderedEventCollection"/>.</typeparam>
	/// <remarks>  
	/// Initializes a new instance of the <see cref="LevelElementCollection{T}"/> class.  
	/// </remarks>  
	/// <param name="parent">The parent <see cref="RDLevel"/> instance associated with this collection.</param>  
	public abstract class LevelElementCollection<T>(RDLevel parent, bool limited) : ICollection<T> where T : OrderedEventCollection
	{
		/// <summary>  
		/// The parent <see cref="RDLevel"/> instance associated with this collection.  
		/// </summary>  
		protected private RDLevel parent = parent;
		/// <summary>  
		/// The internal list of items in the collection.  
		/// </summary>  
		protected private readonly List<T> _items = limited ? new(16) : [];
		/// <inheritdoc/>
		public int Count => _items.Count;
		/// <inheritdoc/>
		public bool IsReadOnly { get; internal set; } = false;

		/// <inheritdoc/>
		public abstract void Add(T item);
		/// <inheritdoc/>
		public void Clear() => _items.Clear();
		/// <inheritdoc/>
		public bool Contains(T item) => _items.Contains(item);
		/// <inheritdoc/>
		public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);
		/// <inheritdoc/>
		public IEnumerator<T> GetEnumerator()
		{
			foreach (var item in _items)
				yield return item;
		}
		/// <summary>  
		/// Gets the index of the specified item in the collection.  
		/// </summary>  
		/// <param name="item">The item to locate in the collection.</param>  
		/// <returns>The zero-based index of the item if found; otherwise, -1.</returns>  
		public int IndexOf(T item) => _items.IndexOf(item);
		/// <inheritdoc/>
		public abstract bool Remove(T item);
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
