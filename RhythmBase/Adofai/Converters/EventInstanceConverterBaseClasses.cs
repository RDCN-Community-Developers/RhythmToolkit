﻿using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Utils;
using RhythmBase.Global.Extensions;
using System.Text;
using System.Text.Json;

namespace RhythmBase.Adofai.Converters;

internal class EventInstanceConverterBaseTileEvent<TEvent> : EventInstanceConverterBaseEvent<TEvent> where TEvent : BaseTileEvent, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		if (base.Read(ref reader, propertyName, ref value, options))
			return true;
		if (propertyName.SequenceEqual("floor"u8))
			value._floor = reader.GetInt32();
		else
			return false;
		return true;
	}
}
internal class EventInstanceConverterBaseTaggedTileEvent<TEvent> : EventInstanceConverterBaseTileEvent<TEvent> where TEvent : BaseTaggedTileEvent, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		if (base.Read(ref reader, propertyName, ref value, options))
			return true;
		if (propertyName.SequenceEqual("eventTag"u8))
			value.EventTag = reader.GetString() ?? "";
		else if (propertyName.SequenceEqual("angleOffset"u8))
			value.AngleOffset = reader.GetSingle();
		else
			return false;
		return true;
	}
	protected override void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
		writer.WriteString("eventTag"u8, value.EventTag);
		writer.WriteNumber("angleOffset"u8, value.AngleOffset);
	}
}
internal class EventInstanceConverterSetFilterAdvanced : EventInstanceConverterBaseTaggedTileEvent<SetFilterAdvanced>
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref SetFilterAdvanced value, JsonSerializerOptions options)
	{
		if (base.Read(ref reader, propertyName, ref value, options))
			return true;
		if (propertyName.SequenceEqual("filter"u8))
			value.Filter = reader.GetString() ?? "";
		else if (propertyName.SequenceEqual("enabled"u8))
			value.Enabled = reader.GetBoolean();
		else if (propertyName.SequenceEqual("disableOthers"u8))
			value.DisableOthers = reader.GetBoolean();
		else if (propertyName.SequenceEqual("duration"u8))
			value.Duration = reader.GetSingle();
		else if (propertyName.SequenceEqual("ease"u8) && EnumConverter.TryParse(reader.ValueSpan, out Global.Components.Easing.EaseType enumValue0))
			value.Ease = enumValue0;
		else if (propertyName.SequenceEqual("targetType"u8) && EnumConverter.TryParse(reader.ValueSpan, out TargetType enumValue1))
			value.TargetType = enumValue1;
		else if (propertyName.SequenceEqual("plane"u8) && EnumConverter.TryParse(reader.ValueSpan, out Plane enumValue2))
			value.Plane = enumValue2;
		else if (propertyName.SequenceEqual("targetTag"u8))
			value.TargetTag = reader.GetString() ?? "";
		else if (propertyName.SequenceEqual("filterProperties"u8))
		{
			ReadOnlySpan<byte> json = [(byte)'{', .. Encoding.UTF8.GetBytes((reader.GetString() ?? "")), (byte)'}'];
			Utf8JsonReader subReader = new(json);
			if(FilterTypeUtils.converters.TryGetValue(value.Filter, out FilterInstanceConverterBase? converter))
				value.FilterProperties = converter.ReadProperties(ref subReader, options);
		}
		else
			return false;
		return true;
	}
	protected override void Write(Utf8JsonWriter writer, ref SetFilterAdvanced value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
		writer.WriteString("filter"u8, value.Filter);
		writer.WriteBoolean("enabled"u8, value.Enabled);
		writer.WriteBoolean("disableOthers"u8, value.DisableOthers);
		if (value.Enabled)
			writer.WriteNumber("duration"u8, value.Duration);
		if (value.Enabled)
			writer.WriteString("ease"u8, value.Ease.ToEnumString());
		writer.WriteString("targetType"u8, value.TargetType.ToEnumString());
		writer.WriteString("plane"u8, value.Plane.ToEnumString());
		writer.WriteString("targetTag"u8, value.TargetTag);
		writer.WritePropertyName("filterProperties"u8);
		{
			using MemoryStream ms = new();
			using Utf8JsonWriter tempWriter = new(ms, new() { SkipValidation = true,});
			if (FilterTypeUtils.converters.TryGetValue(value.Filter, out FilterInstanceConverterBase? converter))
				converter.WriteProperties(tempWriter, value.FilterProperties, options);
			tempWriter.Flush();
			string jsonString = Encoding.UTF8.GetString(ms.ToArray());
			writer.WriteStringValue(jsonString);
		}

	}
}