using RhythmBase.Adofai.Events;
using System.Collections;

namespace RhythmBase.Adofai.Components
{

	public abstract class ADTileCollection : ICollection<ADTile>
	{
		protected ADTileCollection()
		{
			tileOrder = [];
			IsReadOnly = false;
			EndTile = [];
		}
		public int Count => tileOrder.Count;
		public bool IsReadOnly { get; }
		public ADTile EndTile { get; }
		public ADTile this[int index] => index == tileOrder.Count ? EndTile : tileOrder[index];
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
		}
		public void Add(ADTile item) => tileOrder.Add(item);
		public void Clear() => tileOrder.Clear();
		public void CopyTo(ADTile[] array, int arrayIndex) => tileOrder.CopyTo(array, arrayIndex);
		public bool Contains(ADTile item) => tileOrder.Contains(item);
		public bool Remove(ADTile item) => tileOrder.Remove(item);
		public IEnumerator<ADTile> GetEnumerator() => tileOrder.GetEnumerator();
		/// <summary>
		/// Get the index of tile.
		/// </summary>
		/// <param name="item">The index of tile.</param>
		/// <returns></returns>
		public int IndexOf(ADTile item) => item == EndTile ? Count : tileOrder.IndexOf(item);
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		internal List<ADTile> tileOrder;
	}
}
