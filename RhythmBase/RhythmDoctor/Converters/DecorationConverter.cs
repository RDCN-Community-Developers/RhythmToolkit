using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters;

internal class DecorationConverter : JsonConverter<Decoration>
{
	public override Decoration? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");
		Decoration decoration = [];
		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
				break;
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueSpan.SequenceEqual("id"u8))
				{
					reader.Read();
					decoration.Id = reader.GetString() ?? "";
				}
				else if (reader.ValueSpan.SequenceEqual("rooms"u8))
				{
					reader.Read();
					decoration.Room = JsonSerializer.Deserialize<RDSingleRoom>(ref reader, options);
				}
				else if (reader.ValueSpan.SequenceEqual("filename"u8))
				{
					reader.Read();
					decoration.Character = reader.GetString() ?? "";
				}
				else if (reader.ValueSpan.SequenceEqual("character"u8))
				{
					reader.Read();
					string character = reader.GetString() ?? "";
					if (EnumConverter.TryParse(character, out RDCharacters rdc))
						decoration.Character = rdc;
					else
						decoration.Character = character;
				}
				else if (reader.ValueSpan.SequenceEqual("preview"u8))
				{
					reader.Read();
					decoration.Preview = reader.GetBoolean();
				}
				else if (reader.ValueSpan.SequenceEqual("depth"u8))
				{
					reader.Read();
					decoration.Depth = reader.GetInt32();
				}
				else if (reader.ValueSpan.SequenceEqual("filter"u8))
				{
					reader.Read();
					if (EnumConverter.TryParse(reader.ValueSpan, out Filter result))
						decoration.Filter = result;
				}
				else if (reader.ValueSpan.SequenceEqual("visible"u8))
				{
					reader.Read();
					decoration.Visible = reader.GetBoolean();
				}
			}
		}
		return decoration;
	}

	public override void Write(Utf8JsonWriter writer, Decoration value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteString("id"u8, value.Id);
		writer.WriteNumber("row"u8, value.Index);
		writer.WritePropertyName("rooms"u8);
		JsonSerializer.Serialize(writer, value.Room, options);
		if (!value.Character.IsCustom && value.Character.Character is RDCharacters rdc)
			writer.WriteString("character", EnumConverter.ToEnumString(rdc));
		else
			writer.WriteString("filename", value.Character.CustomCharacter);
		writer.WriteBoolean("preview"u8, value.Preview);
		writer.WriteNumber("depth"u8, value.Depth);
		if (value.Filter is not Filter.NearestNeighbor)
			writer.WriteString("filter"u8, EnumConverter.ToEnumString(value.Filter));
		if (!value.Visible)
			writer.WriteBoolean("visible"u8, value.Visible);
		writer.WriteEndObject();
	}
}
