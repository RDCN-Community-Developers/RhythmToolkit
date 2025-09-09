using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Events
{
	public interface IForwardEvent : IBaseEvent
	{
		string ActureType { get; set; }
		JsonElement this[string key] { get; set; }
		string ToString();
	}
}