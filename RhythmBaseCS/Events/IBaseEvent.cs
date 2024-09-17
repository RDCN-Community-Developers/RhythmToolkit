using RhythmBase.Components;
namespace RhythmBase.Events
{
	public interface IBaseEvent
	{
		bool Active { get; set; }
		Beat Beat { get; set; }
		Condition Condition { get; set; }
		Tabs Tab { get; }
		string Tag { get; set; }
		EventType Type { get; }
		int Y { get; set; }
		string ToString();
	}
}