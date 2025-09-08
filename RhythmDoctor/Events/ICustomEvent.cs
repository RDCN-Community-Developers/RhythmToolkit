using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Events
{
	public interface ICustomEvent : IBaseEvent
	{
		bool Active { get; set; }
		string ActureType { get; set; }
		int Bar { get; set; }
		float BeatValue { get; set; }
		Condition? Condition { get; set; }
		string? ConditionRaw { get; set; }
		Dictionary<string, JsonElement> ExtraData { get; set; }
		Tabs Tab { get; }
		string Tag { get; set; }
		EventType Type { get; }
		int Y { get; set; }

		string ToString();
		bool TryConvert(ref BaseEvent? value, ref EventType? type);
		bool TryConvert(ref BaseEvent? value, ref EventType? type, LevelReadOrWriteSettings settings);
	}
}