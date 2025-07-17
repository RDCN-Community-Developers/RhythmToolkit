using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Converters
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
#if NETCOREAPP3_0_OR_GREATER
				string name = value[7..];
#else
				string name = value.Substring(7);
#endif
				ReadJson = name;
			}
			else
			{
#if NETSTANDARD
				ReadJson = string.IsNullOrEmpty(value) ? new() : (RDCharacters)Enum.Parse(typeof(RDCharacters), value);
#else
				ReadJson = string.IsNullOrEmpty(value) ? new() : Enum.Parse<RDCharacters>(value);
#endif
			}
			return ReadJson;
		}
	}
}
