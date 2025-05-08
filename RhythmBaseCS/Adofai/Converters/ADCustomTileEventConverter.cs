using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Global.Settings;

namespace RhythmBase.Adofai.Converters
{
	internal class ADCustomTileEventConverter(ADLevel level, LevelReadOrWriteSettings settings) : ADBaseTileEventConverter<ADCustomTileEvent>(level, settings)
	{
		public override ADCustomTileEvent GetDeserializedObject(JObject jobj, Type objectType, ADCustomTileEvent existingValue, bool hasExistingValue, JsonSerializer serializer) => new()
		{
			Parent = level[jobj["floor"].ToObject<int>()],
			Data = jobj
		};
	}
}
