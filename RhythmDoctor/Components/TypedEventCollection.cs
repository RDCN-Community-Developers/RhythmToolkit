using RhythmBase.RhythmDoctor.Events;
using System.Collections;
namespace RhythmBase.RhythmDoctor.Components
{
	internal class TypedEventCollection<TEvent> : IEnumerable<TEvent> where TEvent : IBaseEvent
	{
		public int Count => list.Count;
		public TypedEventCollection()		{		}
		public void Add(TEvent item)
		{
			list.Add(item);
			int div = (int)item.Type / 64;
			int rem = (int)item.Type % 64;
			_types[div] |= (1ul << rem);
		}
		public bool Remove(TEvent item)
		{
			bool result = list.Remove(item);
			if (!result)
				return result;
			if (!list.Any(i => i.Type == item.Type))
			{
				int div = (int)item.Type / 64;
				int rem = (int)item.Type % 64;
				_types[div] &= ~(1ul << rem);
			}
			return result;
		}
		internal bool ContainsType(EventType type)
		{
			int div = (int)type / 64;
			int rem = (int)type % 64;
			return (_types[div] & (1ul << rem)) != 0;
		}
		internal bool ContainsTypes(EventType[] types)
		{
			foreach (EventType type in types)
			{
				int div = (int)type / 64;
				int rem = (int)type % 64;
				if ((_types[div] & (1ul << rem)) != 0)
					return true;
			}
			return false;
		}
		public bool CompareTo(IBaseEvent item1, IBaseEvent item2) =>
			list.IndexOf((TEvent)(object)item1) < list.IndexOf((TEvent)(object)item2);
		public override string ToString() =>
			string.Format("{0}Count={1}", ContainsType(EventType.SetBeatsPerMinute) || ContainsType(EventType.PlaySong) ? "BPM, " : ContainsType(EventType.SetCrotchetsPerBar) ? "CPB, " : "", list.Count);
		public IEnumerator<TEvent> GetEnumerator() =>
			list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() =>
			list.GetEnumerator();
		private readonly List<TEvent> list = [];
		protected internal ulong[] _types = new ulong[2];
	}
}
