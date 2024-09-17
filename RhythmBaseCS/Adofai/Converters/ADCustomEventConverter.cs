using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Settings;
namespace RhythmBase.Adofai.Converters
{
	internal class ADCustomEventConverter(ADLevel level, LevelReadOrWriteSettings settings) : ADBaseEventConverter<ADCustomEvent>(level, settings)
	{
		public override ADCustomEvent GetDeserializedObject(JObject jobj, Type objectType, ADCustomEvent existingValue, bool hasExistingValue, JsonSerializer serializer) => new()
		{
			Data = jobj
		};
	}
}
