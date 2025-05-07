using RhythmBase.Adofai.Events;
using System.Collections;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents a collection of ADOFAI tiles.
	/// </summary>
	public abstract class ADTileCollection : ICollection<ADTile>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ADTileCollection"/> class.
		/// </summary>
		protected ADTileCollection()
		{
			tileOrder = [];
			IsReadOnly = false;
			EndTile = [];
		}		/// <summary>
		/// Gets the number of tiles in the collection.
		/// </summary>
		public int Count => tileOrder.Count;		/// <summary>
		/// Gets a value indicating whether the collection is read-only.
		/// </summary>
		public bool IsReadOnly { get; }		/// <summary>
		/// Gets the end tile of the collection.
		/// </summary>
		public ADTile EndTile { get; }		/// <summary>
		/// Gets the tile at the specified index.
		/// </summary>
		/// <param name="index">The index of the tile to retrieve.</param>
		/// <returns>The tile at the specified index, or the end tile if the index equals the count.</returns>
		public ADTile this[int index] => index == tileOrder.Count ? EndTile : tileOrder[index];		/// <summary>
		/// Gets an enumerable collection of all events associated with the tiles in the collection.
		/// </summary>
		public virtual IEnumerable<ADBaseEvent> Events
		{
			get
			{
				foreach (ADTile tile in tileOrder)
					foreach (ADBaseTileEvent action in tile)
						yield return action;
				foreach (ADBaseTileEvent action2 in EndTile)
					yield return action2;
			}
		}		/// <summary>
		/// Adds a tile to the collection.
		/// </summary>
		/// <param name="item">The tile to add.</param>
		public void Add(ADTile item) => tileOrder.Add(item);		/// <summary>
		/// Removes all tiles from the collection.
		/// </summary>
		public void Clear() => tileOrder.Clear();		/// <summary>
		/// Copies the elements of the collection to an array, starting at a particular array index.
		/// </summary>
		/// <param name="array">The destination array.</param>
		/// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
		public void CopyTo(ADTile[] array, int arrayIndex) => tileOrder.CopyTo(array, arrayIndex);		/// <summary>
		/// Determines whether the collection contains a specific tile.
		/// </summary>
		/// <param name="item">The tile to locate in the collection.</param>
		/// <returns><c>true</c> if the tile is found; otherwise, <c>false</c>.</returns>
		public bool Contains(ADTile item) => tileOrder.Contains(item);		/// <summary>
		/// Removes the first occurrence of a specific tile from the collection.
		/// </summary>
		/// <param name="item">The tile to remove.</param>
		/// <returns><c>true</c> if the tile was successfully removed; otherwise, <c>false</c>.</returns>
		public bool Remove(ADTile item) => tileOrder.Remove(item);		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An enumerator for the collection.</returns>
		public IEnumerator<ADTile> GetEnumerator() => tileOrder.GetEnumerator();		/// <summary>
		/// Gets the index of a specific tile in the collection.
		/// </summary>
		/// <param name="item">The tile to locate.</param>
		/// <returns>The index of the tile if found; otherwise, -1.</returns>
		public int IndexOf(ADTile item) => item == EndTile ? Count : tileOrder.IndexOf(item);		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An enumerator for the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();		/// <summary>
		/// The internal list that stores the tiles in the collection.
		/// </summary>
		internal List<ADTile> tileOrder;
	}
}
