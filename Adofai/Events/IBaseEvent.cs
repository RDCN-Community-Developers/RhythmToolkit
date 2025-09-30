using System.Text.Json;

namespace RhythmBase.Adofai.Events
{
	public interface IBaseEvent : IEvent
	{
		EventType Type { get; }
		JsonElement this[string key] { get; set; }
	}
}