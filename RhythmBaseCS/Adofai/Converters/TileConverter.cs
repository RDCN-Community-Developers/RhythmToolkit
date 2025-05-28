using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Utils;
namespace RhythmBase.Adofai.Converters
{
	internal class TileConverter(ADLevel level) : JsonConverter<Tile>
	{
		public override void WriteJson(JsonWriter writer, Tile? value, JsonSerializer serializer)
		{
			writer.WriteValue(value?.IsMidSpin == true ? Utils.Utils.MidSpinAngle : value?.Angle ?? 0);
		}

		public override Tile ReadJson(JsonReader reader, Type objectType, Tile? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			float angle = JToken.Load(reader).ToObject<float>();
			if(angle == Utils.Utils.MidSpinAngle)
			{
				return new()
				{
					IsMidSpin = true,
					Parent = level
				};
			}
			return new()
			{
				Angle = JToken.Load(reader).ToObject<float>(),
				Parent = level
			};
		}

		private readonly ADLevel level = level;
	}
}
