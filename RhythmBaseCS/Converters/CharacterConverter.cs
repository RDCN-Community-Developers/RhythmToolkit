using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
namespace RhythmBase.Converters
{
	internal class CharacterConverter : JsonConverter<RDCharacter>
	{
		public override void WriteJson(JsonWriter writer, RDCharacter value, JsonSerializer serializer)
		{
			writer.WriteValue(
				value.IsCustom
				? value.CustomCharacter == null
					? ""
					: $"custom:{value.CustomCharacter}"
				: value.Character.ToString());
		}

		public override RDCharacter ReadJson(JsonReader reader, Type objectType, RDCharacter existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string value = JToken.ReadFrom(reader).ToObject<string>()!;
			RDCharacter ReadJson;
			if (value.StartsWith("custom:"))
			{
				string name = value[7..];
				ReadJson = name;
			}
			else
			{
				ReadJson = Enum.Parse<Characters>(value);
			}
			return ReadJson;
		}
	}
}
