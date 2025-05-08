using RhythmBase.RhythmDoctor.Events;
using System.Collections;
namespace RhythmBase.RhythmDoctor.Components
{
	internal class TypedEventCollection<TEvent> : IEnumerable<TEvent> where TEvent : IBaseEvent
	{
		public TypedEventCollection()
		{
			list = [];
			_types = [];
		}
		public void Add(TEvent item)
		{
			list.Add(item);
			_types.Add(item.Type);
		}
		public bool Remove(TEvent item)
		{
			_types.Remove(item.Type);
			return list.Remove(item);
		}
		public bool BeforeThan(IBaseEvent item1, IBaseEvent item2) =>
			list.IndexOf((TEvent)(object)item1) < list.IndexOf((TEvent)(object)item2);
		public override string ToString() =>
			string.Format("{0}Count={1}", _types.Contains(EventType.SetBeatsPerMinute) || _types.Contains(EventType.PlaySong) ? "BPM, " : _types.Contains(EventType.SetCrotchetsPerBar) ? "CPB, " : "", list.Count);
		public IEnumerator<TEvent> GetEnumerator() =>
			list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() =>
			list.GetEnumerator();
		private readonly List<TEvent> list;
		protected internal HashSet<EventType> _types;
	}
}
