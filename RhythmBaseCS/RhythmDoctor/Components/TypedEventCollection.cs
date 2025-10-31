using RhythmBase.RhythmDoctor.Events;
using System.Collections;
namespace RhythmBase.RhythmDoctor.Components
{
	internal class TypedEventCollection<TEvent> : IEnumerable<TEvent> where TEvent : IBaseEvent
	{
		private const int bw = sizeof(ulong) * 8;
		public int Count => list.Count;
		public TypedEventCollection() { }
		public bool Add(TEvent item)
		{
			if (list.Contains(item))
				return false;
			list.Add(item);
			int div = (int)item.Type / bw;
			int rem = (int)item.Type % bw;
			_types[div] |= (1ul << rem);
			return true;
		}
		public bool Remove(TEvent item)
		{
			bool result = list.Remove(item);
			if (!result)
				return false;
			if (!list.Any(i => i.Type == item.Type))
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
		public override string ToString()
		{
			string result = "";
			if (ContainsType(EventType.SetCrotchetsPerBar))
				result += "CPB,";
			if(ContainsType(EventType.SetBeatsPerMinute))
				result += "BPM,";
			result += $"Count={list.Count}";
			return result;
		}
		public IEnumerator<TEvent> GetEnumerator() =>
			list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() =>
			list.GetEnumerator();
		private readonly List<TEvent> list = [];
		private readonly ulong[] _types = new ulong[2];
	}
}
