using RhythmBase.Global.Serialization;
using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Serialization;

[JsonConverterFor(typeof(SoundCollection))]
internal class SoundCollectionConverter : JsonConverter<SoundCollection>
{
	public override SoundCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		JsonException.ThrowIfNotMatch(ref reader, JsonTokenType.StartArray);
		List<Audio> audios = [];
		List<SoundType> soundTypes = [];
		while (reader.Read() && reader.TokenType is not JsonTokenType.EndArray)
		{
			JsonException.ThrowIfNotMatch(ref reader, JsonTokenType.StartObject);
			bool soundTypeFound = false;
			Audio item = new();
			while (reader.Read() && reader.TokenType is not JsonTokenType.EndObject)
			{
				JsonException.ThrowIfNotMatch(ref reader, JsonTokenType.PropertyName);
				if (reader.ValueTextEquals("groupSubtype"u8) && reader.Read() && EnumConverter.TryParse(ref reader, out SoundType result1))
				{
					soundTypes.Add(result1);
					soundTypeFound = true;
				}
				else if (reader.ValueTextEquals("used"u8) && reader.Read())
				{
					bool used = reader.GetBoolean();
					if (!used)
					{
						item = null!;
						while (reader.Read() && reader.TokenType is not JsonTokenType.EndObject) { }
						break;
					}
				}
				else if (reader.ValueTextEquals("filename"u8) && reader.Read())
					item.Filename = reader.GetString() ?? string.Empty;
				else if (reader.ValueTextEquals("volume"u8) && reader.Read())
					item.Volume = reader.GetInt32();
				else if (reader.ValueTextEquals("pitch"u8) && reader.Read())
					item.Pitch = reader.GetInt32();
				else if (reader.ValueTextEquals("pan"u8) && reader.Read())
					item.Pan = reader.GetInt32();
				else if (reader.ValueTextEquals("offset"u8) && reader.Read())
					item.Offset = TimeSpan.FromMilliseconds(reader.GetSingle());
				else
				{
#if DEBUG
					Console.WriteLine($"Found unknown property '{reader.GetString()}' in {nameof(SoundCollection)}");
#endif
					reader.Skip();
				}
			}
			if (!soundTypeFound)
				soundTypes.Add(SoundType.ClapSoundP1Classic);
			audios.Add(item);
		}
		SoundCollection collection = new SoundCollection(soundTypes.ToArray());
		collection._values = [.. audios];
		return collection;
	}

	public override void Write(Utf8JsonWriter writer, SoundCollection value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();
		foreach (KeyValuePair<SoundType, Audio?> kvp in value)
		{
			Audio? item = kvp.Value;
			writer.WriteStartObject();
			if (item is null)
				writer.WriteBoolean("used"u8, false);
			else
			{
				if (kvp.Key is not SoundType.ClapSoundP1Classic)
					writer.WriteString("groupSubtype"u8, kvp.Key.ToEnumUtf8String());
				writer.WriteString("filename"u8, item.Filename);
				if (item.Volume != 100)
					writer.WriteNumber("volume"u8, item.Volume);
				if (item.Pitch != 100)
					writer.WriteNumber("pitch"u8, item.Pitch);
				if (item.Pan != 0)
					writer.WriteNumber("pan"u8, item.Pan);
				if (item.Offset != TimeSpan.Zero)
					writer.WriteNumber("offset"u8, item.Offset.TotalMilliseconds);
			}
			writer.WriteEndObject();
		}
		writer.WriteEndArray();
	}
}
