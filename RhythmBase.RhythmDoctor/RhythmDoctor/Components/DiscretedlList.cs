namespace RhythmBase.RhythmDoctor.Components;

public class DiscretedlList<T>
{
	private readonly SortedDictionary<int, T> _dict = new();
	public int Count => _dict.Count;
	public DiscretedlList() { }
	internal void Insert(T value, int index)
	{
		_dict[index] = value;
	}
	public void Add(T value)
	{
		int nextIndex = _dict.Count == 0 ? 0 : _dict.Keys.Max() + 1;
		_dict[nextIndex] = value;
	}
	public void RemoveAt(int index)
	{
		if (!_dict.Remove(index))
			throw new ArgumentOutOfRangeException(nameof(index));
	}
	public T this[int index]
	{
		get => _dict[index];
		set => _dict[index] = value;
	}
}
