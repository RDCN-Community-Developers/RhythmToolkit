using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RowConverter : JsonConverter<Row>
	{
		public override Row? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			Row result = [];
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueSpan.SequenceEqual("character"u8))
					{
						reader.Read();
						result.Character = JsonSerializer.Deserialize<RDCharacter>(ref reader, options);
					}
					else if (reader.ValueSpan.SequenceEqual("rowType"u8))
					{
						reader.Read();
						if (EnumConverter.TryParse(reader.ValueSpan, out RowTypes value))
							result.RowType = value;
					}
					else if (reader.ValueSpan.SequenceEqual("rooms"u8))
					{
						reader.Read();
						result.Rooms = JsonSerializer.Deserialize<RDSingleRoom>(ref reader, options);
					}
					else if (reader.ValueSpan.SequenceEqual("hideAtStart"u8))
					{
						reader.Read();
						result.HideAtStart = reader.GetBoolean();
					}
					else if (reader.ValueSpan.SequenceEqual("player"u8))
					{
						reader.Read();
						if(EnumConverter.TryParse(reader.ValueSpan, out PlayerType value))
						result.Player = value;
					}
					else if (reader.ValueSpan.SequenceEqual("muteBeats"u8))
					{
						reader.Read();
						result.MuteBeats = reader.GetBoolean();
					}
					else if (reader.ValueSpan.SequenceEqual("rowToMimic"u8))
					{
						reader.Read();
						result.RowToMimic = reader.GetSByte();
					}
					else if (reader.ValueSpan.SequenceEqual("pulseSound"u8))
					{
						reader.Read();
						result.PulseSound = reader.GetString() ?? "";
					}
					else if (reader.ValueSpan.SequenceEqual("pulseSoundVolume"u8))
					{
						reader.Read();
						result.PulseSoundVolume = reader.GetInt32();
					}
					else if (reader.ValueSpan.SequenceEqual("pulseSoundPitch"u8))
					{
						reader.Read();
						result.PulseSoundPitch = reader.GetInt32();
					}
					else if (reader.ValueSpan.SequenceEqual("pulseSoundPan"u8))
					{
						reader.Read();
						result.PulseSoundPan = reader.GetInt32();
					}
				}
			}
			return result;
		}

		public override void Write(Utf8JsonWriter writer, Row value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("character"u8);
			JsonSerializer.Serialize(writer, value.Character, options);

			writer.WriteString("rowType"u8, EnumConverter.ToEnumString(value.RowType));
			writer.WriteNumber("row"u8, value.Index);

			writer.WritePropertyName("rooms"u8);
			JsonSerializer.Serialize(writer, value.Rooms, options);

			writer.WriteBoolean("hideAtStart"u8, value.HideAtStart);
			writer.WriteString("player"u8, EnumConverter.ToEnumString(value.Player));
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
