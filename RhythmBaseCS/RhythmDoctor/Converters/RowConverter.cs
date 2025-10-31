using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RowConverter : JsonConverter<Row>
	{
		private static CharacterConverter CharacterConverter = new CharacterConverter();
		public override Row? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			Row result = [];
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType != JsonTokenType.PropertyName)
									throw new JsonException("Expected PropertyName token");
				ReadOnlySpan<byte> propertyName = reader.ValueSpan;
				reader.Read();
				if (propertyName.SequenceEqual("character"u8))
					result.Character = CharacterConverter.Read(ref reader, typeof(RDCharacter), options);//JsonSerializer.Deserialize<RDCharacter>(ref reader, options);
				else if (propertyName.SequenceEqual("cpuMarker"u8) && EnumConverter.TryParse(reader.ValueSpan, out RDCharacters value0))
					result.CpuMarker = value0;
				else if (propertyName.SequenceEqual("rowType"u8) && EnumConverter.TryParse(reader.ValueSpan, out RowTypes value1))
					result.RowType = value1;
				else if (propertyName.SequenceEqual("rooms"u8))
					result.Rooms = JsonSerializer.Deserialize<RDSingleRoom>(ref reader, options);
				else if (propertyName.SequenceEqual("hideAtStart"u8))
					result.HideAtStart = reader.GetBoolean();
				else if (propertyName.SequenceEqual("player"u8) && EnumConverter.TryParse(reader.ValueSpan, out PlayerType value2))
					result.Player = value2;
				else if (propertyName.SequenceEqual("muteBeats"u8))
					result.MuteBeats = reader.GetBoolean();
				else if (propertyName.SequenceEqual("rowToMimic"u8))
					result.RowToMimic = reader.GetSByte();
				else if (propertyName.SequenceEqual("pulseSound"u8))
					result.PulseSound = reader.GetString() ?? "";
				else if (propertyName.SequenceEqual("pulseSoundVolume"u8))
					result.PulseSoundVolume = reader.GetInt32();
				else if (propertyName.SequenceEqual("pulseSoundPitch"u8))
					result.PulseSoundPitch = reader.GetInt32();
				else if (propertyName.SequenceEqual("pulseSoundPan"u8))
					result.PulseSoundPan = reader.GetInt32();
				else if (propertyName.SequenceEqual("pulseSoundOffset"u8))
					result.PulseSoundOffset = TimeSpan.FromMilliseconds(reader.GetDouble());
			}
			return result;
		}

		public override void Write(Utf8JsonWriter writer, Row value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("character"u8);
			CharacterConverter.Write(writer, value.Character, options);

			if (value.Player == PlayerType.CPU)
			{
				writer.WritePropertyName("cpuMarker"u8);
				CharacterConverter.Write(writer, value.CpuMarker, options);
			}

			writer.WriteString("rowType"u8, EnumConverter.ToEnumString(value.RowType));
			writer.WriteNumber("row"u8, value.Index);

			writer.WritePropertyName("rooms"u8);
			JsonSerializer.Serialize(writer, value.Rooms, options);

			if (value.HideAtStart)
				writer.WriteBoolean("hideAtStart"u8, value.HideAtStart);
			writer.WriteString("player"u8, EnumConverter.ToEnumString(value.Player));
			if (value.MuteBeats)
				writer.WriteBoolean("muteBeats"u8, value.MuteBeats);

			if (value.RowToMimic >= 0)
				writer.WriteNumber("rowToMimic"u8, value.RowToMimic);

			writer.WriteString("pulseSound"u8, value.PulseSound ?? "");

			if (value.PulseSoundVolume != 100)
				writer.WriteNumber("pulseSoundVolume"u8, value.PulseSoundVolume);

			if (value.PulseSoundPitch != 100)
				writer.WriteNumber("pulseSoundPitch"u8, value.PulseSoundPitch);

			if (value.PulseSoundPan != 0)
				writer.WriteNumber("pulseSoundPan"u8, value.PulseSoundPan);

			if (value.PulseSoundOffset != TimeSpan.Zero)
				writer.WriteNumber("pulseSoundOffset"u8, (int)value.PulseSoundOffset.TotalMilliseconds);

			writer.WriteEndObject();
		}
	}
}
