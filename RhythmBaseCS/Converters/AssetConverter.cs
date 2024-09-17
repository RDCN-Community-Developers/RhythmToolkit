using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Assets;
using RhythmBase.Components;

namespace RhythmBase.Converters
{
	internal class AssetConverter<TAsset>(RDLevel level) : JsonConverter<Asset<TAsset>> where TAsset : IAssetFile
	{
		public override void WriteJson(JsonWriter writer, Asset<TAsset> value, JsonSerializer serializer) => writer.WriteValue(value.Name);
		public override Asset<TAsset> ReadJson(JsonReader reader, Type objectType, Asset<TAsset> existingValue, bool hasExistingValue, JsonSerializer serializer) => new(level.Manager)
		{
			Name = JToken.Load(reader).ToObject<string>() ?? "",
		};
		private readonly RDLevel level = level;
	}
}
