using RhythmBase.Adofai.Events;
using System.Collections;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents a strongly-typed list of events that implements <see cref="IEnumerable{T}"/>.
	/// </summary>
	/// <typeparam name="TEvent">The type of event, which must inherit from <see cref="BaseEvent"/>.</typeparam>
	public class TypedEventCollection<TEvent> : IEnumerable<TEvent> where TEvent : IBaseEvent
	{
		private const int bw = sizeof(ulong) * 8;
		/// <summary>
		/// Gets the number of elements contained in the list.
		/// </summary>
		public int Count => list.Count;
		/// <summary>
		/// Initializes a new instance of the <see cref="TypedEventCollection{TEvent}"/> class.
		/// </summary>
		public TypedEventCollection() { }
		/// <summary>
		/// Adds an event to the list.
		/// </summary>
		/// <param name="item">The event to add.</param>
		public virtual bool Add(TEvent item)
		{
			if (item is ISingleEvent && ContainsType(item.Type))
				return false;
			list.Add(item);
			int div = (int)item.Type / bw;
			int rem = (int)item.Type % bw;
			_types[div] |= (1ul << rem);
			return true;
		}
		/// <summary>
		/// Removes an event from the list.
		/// </summary>
		/// <param name="item">The event to remove.</param>
		/// <returns>The result of the removal operation.</returns>
		public virtual bool Remove(TEvent item)
		{
			bool result = list.Remove(item);
			if (!result)
				return false;
			if(!list.Any(i => i.Type == item.Type))
			{
				int div = (int)item.Type / bw;
				int rem = (int)item.Type % bw;
				_types[div] &= ~(1ul << rem);
			}
			return true;
		}
		internal bool ContainsType(EventType type)
		{
			int div = (int)type / bw;
			int rem = (int)type % bw;
			return (_types[div] & (1ul << rem)) != 0;
		}
		internal bool ContainsTypes(EventType[] types)
		{
			foreach (EventType type in types)
			{
				int div = (int)type / bw;
				int rem = (int)type % bw;
				if ((_types[div] & (1ul << rem)) != 0)
					return true;
			}
			return false;
		}
		internal bool CompareTo(IBaseEvent item1, IBaseEvent item2) =>
			list.IndexOf((TEvent)(object)item1) < list.IndexOf((TEvent)(object)item2);

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that contains the count of events in the list.</returns>
		public override string ToString() => $"Count = {list.Count}";
		/// <summary>
		/// Returns an enumerator that iterates through the list.
		/// </summary>
		/// <returns>An enumerator for the list.</returns>
		public IEnumerator<TEvent> GetEnumerator() => list.GetEnumerator();
		/// <summary>
		/// Returns an enumerator that iterates through the list.
		/// </summary>
		/// <returns>An enumerator for the list.</returns>
		IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
		/// <summary>
		/// The internal list that stores the events.
		/// </summary>
		private readonly List<TEvent> list = [];
		/// <summary>
		/// A set of event types contained in the list.
		/// </summary>
		private readonly ulong[] _types =  new ulong[2];
	}
}
