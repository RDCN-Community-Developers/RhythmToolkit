﻿using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;

/* 项目“RhythmBase (net8.0)”的未合并的更改
在此之前:
using RhythmBase.RhythmDoctor.Events;
在此之后:
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Converters.Events;
using RhythmBase.RhythmDoctor.Events;
*/
using RhythmBase.RhythmDoctor.Events;
using System.Text;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal abstract class EventInstantConverterBase
	{
		public abstract IBaseEvent ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options);
		public abstract void WriteProperties(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options);
	}
	internal abstract class EventInstantConverterBaseEvent<TEvent> : EventInstantConverterBase where TEvent : IBaseEvent, new()
	{
		public override sealed IBaseEvent ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			TEvent value = new();
			int bar = 1;
			float beat = 1;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					value.Beat = new(bar, beat);
					return value;
				}
				if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException("Expected property name");
				ReadOnlySpan<byte> propertyName = reader.ValueSpan;
				if (propertyName.IsEmpty)
					throw new JsonException("Property name cannot be null");
				reader.Read();
				if (propertyName.SequenceEqual("bar"u8))
					bar = reader.GetInt32();
				else if (propertyName.SequenceEqual("beat"u8))
					beat = reader.GetSingle();
				else if (propertyName.SequenceEqual("type"u8))
					continue;
				else if (!Read(ref reader, propertyName, ref value, options))
					value[
#if NET8_0_OR_GREATER
						Encoding.UTF8.GetString(propertyName)
#elif NETSTANDARD2_0_OR_GREATER
						Encoding.UTF8.GetString(propertyName.ToArray())
#endif
						] = JsonElement.ParseValue(ref reader);
			}
			return value;
		}
		public override sealed void WriteProperties(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options)
		{
			TEvent v = (TEvent)value;
			writer.WriteStartObject();
			Write(writer, ref v, options);
			foreach(var kv in ((BaseEvent)(IBaseEvent)v)._extraData)
			{
				writer.WritePropertyName(kv.Key);
				writer.WriteRawValue(kv.Value.GetRawText());
			}
			writer.WriteEndObject();
		}
		protected virtual bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
		{
			bool result = true;
			if (propertyName.SequenceEqual("y"u8))
				value.Y = reader.GetInt32();
			else if (propertyName.SequenceEqual("tag"u8))
				value.Tag = reader.GetString() ?? string.Empty;
			else if (propertyName.SequenceEqual("runTag"u8))
				value.RunTag = reader.GetBoolean();
			else if (propertyName.SequenceEqual("if"u8))
				value.Condition = Condition.Load(reader.GetString() ?? string.Empty);
			else if (propertyName.SequenceEqual("active"u8))
				value.Active = reader.GetBoolean();
			else
				result = false;
			return result;
		}
		protected virtual void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
		{
			writer.WriteNumber("bar"u8, value.Beat.BarBeat.bar);
			if (value is not IBarBeginningEvent)
				writer.WriteNumber("beat"u8, value.Beat.BarBeat.beat);
			writer.WriteString("type"u8, EnumConverter.ToEnumString(value.Type));
			if (value is not BaseDecorationAction)
				writer.WriteNumber("y"u8, value.Y);
			if (!string.IsNullOrEmpty(value.Tag))
				writer.WriteString("tag"u8, value.Tag);
			if (value.RunTag)
				writer.WriteBoolean("runTag"u8, true);
			if (value.Condition != null)
				writer.WriteString("if"u8, value.Condition.Serialize());
			if (!value.Active)
				writer.WriteBoolean("active"u8, false);
		}
	}
}