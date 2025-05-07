using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
namespace RhythmBase.Adofai.Converters
{
	internal class ADTileConverter(ADLevel level) : JsonConverter<ADTile>
	{
		public override void WriteJson(JsonWriter writer, ADTile? value, JsonSerializer serializer) => throw new NotImplementedException();
		public override ADTile ReadJson(JsonReader reader, Type objectType, ADTile? existingValue, bool hasExistingValue, JsonSerializer serializer) => new()
		{
			Angle = JToken.Load(reader).ToObject<float>(),
			Parent = level
		};
		private readonly ADLevel level = level;
	}
}
