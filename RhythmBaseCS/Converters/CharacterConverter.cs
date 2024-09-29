using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Assets;
using RhythmBase.Components;
namespace RhythmBase.Converters
{
	internal class CharacterConverter(RDLevel level, HashSet<SpriteFile> assets) : JsonConverter<RDCharacter>
	{
		public override void WriteJson(JsonWriter writer, RDCharacter value, JsonSerializer serializer)
		{
			if (value.IsCustom)
			{
				writer.WriteValue(value.CustomCharacter == null ? "[Null]" : $"custom:{value.CustomCharacter?.Name}");
			}
			else
			{
				writer.WriteValue(value.Character.ToString());
			}
		}

		public override RDCharacter ReadJson(JsonReader reader, Type objectType, RDCharacter? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string value = JToken.ReadFrom(reader).ToObject<string>()!;
			RDCharacter ReadJson;
			if (value.StartsWith("custom:"))
			{
				string name = value[7..];
				ReadJson = new RDCharacter(level, name);
			}
			else
			{
				ReadJson = new RDCharacter(level, Enum.Parse<Characters>(value));
			}
			return ReadJson;
		}

		private readonly RDLevel level = level;

		private readonly HashSet<SpriteFile> assets = assets;
	}
}
