using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
namespace RhythmBase.Adofai.Converters
{
	internal class CustomTileEventConverter(ADLevel level, LevelReadOrWriteSettings settings) : BaseTileEventConverter<CustomTileEvent>(level, settings)
	{
		public override CustomTileEvent GetDeserializedObject(JObject jobj, Type objectType, CustomTileEvent? existingValue, bool hasExistingValue, JsonSerializer serializer) => new()
		{
			Parent = level[jobj["floor"]?.ToObject<int>() ?? throw new InvalidRDBeatException()],
			Data = jobj
		};
	}
}
