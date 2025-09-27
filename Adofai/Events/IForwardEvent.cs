using System.Text.Json;

namespace RhythmBase.Adofai.Events
{
	public interface IForwardEvent : IBaseEvent
	{
		string ActureType { get; }
		JsonElement Data { get; set; }
	}
}