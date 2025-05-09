using RhythmBase.Adofai.Events;
using System.Collections;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents a strongly-typed list of events that implements <see cref="IEnumerable{T}"/>.
	/// </summary>
	/// <typeparam name="TEvent">The type of event, which must inherit from <see cref="BaseEvent"/>.</typeparam>
	public class ADTypedList<TEvent> : IEnumerable<TEvent> where TEvent : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ADTypedList{TEvent}"/> class.
		/// </summary>
		public ADTypedList()
		{
			list = [];
			_types = [];
		}		/// <summary>
		/// Adds an event to the list.
		/// </summary>
		/// <param name="item">The event to add.</param>
		public virtual void Add(TEvent item)
		{
			list.Add(item);
			_types.Add(item.Type);
		}		/// <summary>
		/// Removes an event from the list.
		/// </summary>
		/// <param name="item">The event to remove.</param>
		/// <returns>The result of the removal operation.</returns>
		public virtual bool Remove(TEvent item)
		{
			_types.Remove(item.Type);
			return list.Remove(item);
		}		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that contains the count of events in the list.</returns>
		public override string ToString() => string.Format("Count = {0}", list.Count);		/// <summary>
		/// Returns an enumerator that iterates through the list.
		/// </summary>
		/// <returns>An enumerator for the list.</returns>
		public IEnumerator<TEvent> GetEnumerator() => list.GetEnumerator();		/// <summary>
		/// Returns an enumerator that iterates through the list.
		/// </summary>
		/// <returns>An enumerator for the list.</returns>
		IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();		/// <summary>
		/// The internal list that stores the events.
		/// </summary>
		private readonly List<TEvent> list;		/// <summary>
		/// A set of event types contained in the list.
		/// </summary>
		protected internal HashSet<EventType> _types;
	}
}
