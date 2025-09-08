using RhythmBase.RhythmDoctor.Events;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters.Events
{
	[JsonSerializable(typeof(IBaseEvent))]
	internal partial class AppJsonContext : JsonSerializerContext	{	}
}
