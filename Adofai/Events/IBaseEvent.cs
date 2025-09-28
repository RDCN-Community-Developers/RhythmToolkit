using System.Text.Json;

namespace RhythmBase.Adofai.Events
{
	public interface IBaseEvent
	{
		EventType Type { get; }
		JsonElement this[string key] { get; set; }
	}
}