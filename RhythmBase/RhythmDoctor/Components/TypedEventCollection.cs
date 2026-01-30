using RhythmBase.RhythmDoctor.Events;
using System.Collections;
namespace RhythmBase.RhythmDoctor.Components
{
	internal class TypedEventCollection<TEvent> : IEnumerable<TEvent> where TEvent : IBaseEvent
	{
		public int Count => list.Count;
		public TypedEventCollection() { }
		public bool Add(TEvent item)
		{
			if (list.Contains(item))
				return false;
			list.Add(item);
			_types.Add(item.Type);
			return true;
		}
		public bool Remove(TEvent item)
		{
			bool result = list.Remove(item);
			if (!result)
				return false;
			if (!list.Any(i => i.Type == item.Type))
				_types.Remove(item.Type);
			return true;
		}
		internal bool ContainsType(EventType type) => _types.Contains(type);
		internal bool ContainsTypes(EventType[] types) => _types.ContainsAny(types);
		internal bool ContainsTypes(ReadOnlyEnumCollection<EventType> types) => _types.AsReadOnly().ContainsAny(types);
		internal bool CompareTo(IBaseEvent item1, IBaseEvent item2) =>
	list.IndexOf((TEvent)(object)item1) < list.IndexOf((TEvent)(object)item2);
		public override string ToString()
		{
			string result = "";
			if (ContainsType(EventType.SetCrotchetsPerBar))
				result += "CPB,";
			if (ContainsType(EventType.SetBeatsPerMinute))
				result += "BPM,";
			result += $"Count={list.Count}";
			return result;
		}
		public IEnumerator<TEvent> GetEnumerator() =>
			list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() =>
			list.GetEnumerator();
		private readonly List<TEvent> list = [];
		private EnumCollection<EventType> _types = new(2);
	}
}
