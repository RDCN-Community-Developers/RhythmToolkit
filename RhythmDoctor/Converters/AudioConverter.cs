﻿using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class AudioConverter : JsonConverter<RDAudio>
	{
		public override RDAudio? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType != JsonTokenType.StartObject)
			{
				throw new JsonException("Expected StartObject token");
			}
			RDAudio audio = new();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					return audio;
				}
				if (reader.TokenType != JsonTokenType.PropertyName)
				{
					throw new JsonException("Expected PropertyName token");
				}
				var propertyName = reader.ValueSpan;
				reader.Read();
				if (propertyName.SequenceEqual("filename"u8))
					audio.Filename = reader.GetString() ?? "";
				else if (propertyName.SequenceEqual("volume"u8))
					audio.Volume = reader.GetInt32();
				else if (propertyName.SequenceEqual("pitch"u8))
					audio.Pitch = reader.GetInt32();
				else if (propertyName.SequenceEqual("pan"u8))
					audio.Pan = reader.GetInt32();
				else if (propertyName.SequenceEqual("offset"u8))
					audio.Offset = TimeSpan.FromMilliseconds(reader.GetDouble());
				else
					throw new JsonException($"Unknown property: {reader.GetString()}");
			}
			throw new JsonException("Expected EndObject token");
		}

		public override void Write(Utf8JsonWriter writer, RDAudio value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteString("filename"u8, value.Filename);
			if(value.Volume != 100)
				writer.WriteNumber("volume"u8, value.Volume);
			if(value.Pitch != 100)
				writer.WriteNumber("pitch"u8, value.Pitch);
			if(value.Pan != 0)
				writer.WriteNumber("pan"u8, value.Pan);
			if(value.Offset != TimeSpan.Zero)
				writer.WriteNumber("offset"u8, value.Offset.TotalMilliseconds);
			writer.WriteEndObject();
		}
	}
}
