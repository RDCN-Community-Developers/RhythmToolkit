using RhythmBase.Adofai.Events;
using System.Collections;
namespace RhythmBase.Adofai.Components
{
	public class ADTypedList<TEvent> : IEnumerable<TEvent> where TEvent : ADBaseEvent
	{
		public ADTypedList()
		{
			list = [];
			_types = [];
		}
		public void Add(TEvent item)
		{
			list.Add(item);
			_types.Add(item.Type);
		}
		public object Remove(TEvent item)
		{
			_types.Remove(item.Type);
			return list.Remove(item);
		}
		public override string ToString() => string.Format("Count = {0}", list.Count);
		public IEnumerator<TEvent> GetEnumerator() => list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
		private readonly List<TEvent> list;
		protected internal HashSet<ADEventType> _types;
	}
}
