using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class CharacterConverter : JsonConverter<RDCharacter>
	{
		public override void Write(Utf8JsonWriter writer, RDCharacter value, JsonSerializerOptions serializer)
		{
			writer.WriteStringValue(
				value.IsCustom
				? value.CustomCharacter == null
					? ""
					: $"custom:{value.CustomCharacter}"
				: value.Character.ToString());
		}
		public override RDCharacter Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
		{
			string value = reader.GetString();
			if (string.IsNullOrEmpty(value))
				return default;
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
				EnumConverter.TryParse(value, out RDCharacters character);
#if NETSTANDARD
				ReadJson = string.IsNullOrEmpty(value) ? new() : character;
#else
				ReadJson = string.IsNullOrEmpty(value) ? new() : Enum.Parse<RDCharacters>(value);
#endif
			}
			return ReadJson;
		}
	}
}
