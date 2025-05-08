using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Global.Settings;

namespace RhythmBase.Adofai.Converters
{
	internal class ADCustomEventConverter(ADLevel level, LevelReadOrWriteSettings settings) : ADBaseEventConverter<CustomEvent>(level, settings)
	{
		public override CustomEvent GetDeserializedObject(JObject jobj, Type objectType, CustomEvent existingValue, bool hasExistingValue, JsonSerializer serializer) => new()
		{
			Data = jobj
		};
	}
}
