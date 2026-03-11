using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Converters;

internal abstract class EventInstanceConverterBaseRowAction<TEvent> : EventInstanceConverterBaseEvent<TEvent> where TEvent : BaseRowAction, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		if (base.Read(ref reader, propertyName, ref value, options))
			return true;
		if (propertyName.SequenceEqual("row"u8))
			value._row = reader.GetInt32();
		else
			return false;
		return true;
	}
	protected override void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
		writer.WriteNumber("row"u8, value.Parent?.Index ?? value._row);
	}
}
internal abstract class EventInstanceConverterBaseBeat<TEvent> : EventInstanceConverterBaseRowAction<TEvent> where TEvent : BaseBeat, new()
{
}
internal abstract class EventInstanceConverterBaseDecorationAction<TEvent> : EventInstanceConverterBaseEvent<TEvent> where TEvent : BaseDecorationAction, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		if (base.Read(ref reader, propertyName, ref value, options))
			return true;
		if (propertyName.SequenceEqual("target"u8))
			value._decoId = reader.GetString() ?? "";
		else
			return false;
		return true;
	}
	protected override void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
		if (value is not Comment cmt || cmt.CustomTab == Tab.Decorations)
			writer.WriteString("target"u8, value.Parent?.Id ?? value._decoId);
	}
}
internal abstract class EventInstanceConverterBaseRowAnimation<TEvent> : EventInstanceConverterBaseRowAction<TEvent> where TEvent : BaseRowAnimation, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		return base.Read(ref reader, propertyName, ref value, options);
	}
	protected override void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
	}
}
internal abstract class EventInstanceConverterBaseBeatsPerMinute<TEvent> : EventInstanceConverterBaseEvent<TEvent> where TEvent : BaseBeatsPerMinute, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		return base.Read(ref reader, propertyName, ref value, options);
	}
	protected override void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
	}
}
internal abstract class EventInstanceConverterBaseWindowEvent<TEvent> : EventInstanceConverterBaseEvent<TEvent> where TEvent : BaseWindowEvent, new()
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
	{
		return base.Read(ref reader, propertyName, ref value, options);
	}
	protected override void Write(Utf8JsonWriter writer, ref TEvent value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
	}
}
internal class EventInstanceConverterSetVFXPreset : EventInstanceConverterBaseEvent<SetVFXPreset>
{
	protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref SetVFXPreset value, JsonSerializerOptions options)
	{
		if (base.Read(ref reader, propertyName, ref value, options))
			return true;
		if (propertyName.SequenceEqual("rooms"u8))
			value.Rooms = JsonSerializer.Deserialize<Components.RDRoom>(ref reader, options);
		else if (propertyName.SequenceEqual("preset"u8))
			if (reader.TokenType is JsonTokenType.String && EnumConverter.TryParse(reader.ValueSpan, out VfxPreset enumValue0))
				value.Preset = enumValue0;
			else if (reader.TokenType is JsonTokenType.Number && reader.TryGetInt32(out int intValue0))
				value.Preset = (VfxPreset)intValue0;
			else
				value.Preset = default;
		else if (propertyName.SequenceEqual("enable"u8))
			if (reader.TokenType is JsonTokenType.True or JsonTokenType.False)
				value.Enable = reader.GetBoolean();
			else if (reader.TokenType is JsonTokenType.String)
				value.Enable = "Enabled" == reader.GetString();
			else
				value.Enable = false;
		else if (propertyName.SequenceEqual("threshold"u8))
			value.Threshold = reader.GetSingle();
		else if (propertyName.SequenceEqual("intensity"u8))
			value.Intensity = reader.GetSingle();
		else if (propertyName.SequenceEqual("color"u8))
			value.Color = JsonSerializer.Deserialize<Components.PaletteColor>(ref reader, options);
		else if (propertyName.SequenceEqual("floatX"u8))
		{
			if (reader.TokenType is not JsonTokenType.Null)
			{
				var p = value.Amount;
				p.X = reader.GetSingle();
				value.Amount = p;
			}
		}
		else if (propertyName.SequenceEqual("floatY"u8))
		{
			if (reader.TokenType is not JsonTokenType.Null)
			{
				var p = value.Amount;
				p.Y = reader.GetSingle();
				value.Amount = p;
			}
		}
		else if (propertyName.SequenceEqual("amount"u8))
			value.Amount = JsonSerializer.Deserialize<Global.Components.Vector.RDPoint>(ref reader, options);
		else if (propertyName.SequenceEqual("speedPerc"u8))
			value.SpeedPercentage = reader.GetSingle();
		else if (propertyName.SequenceEqual("ease"u8))
			if (reader.TokenType is JsonTokenType.String && EnumConverter.TryParse(reader.ValueSpan, out Global.Components.Easing.EaseType enumValue1))
				value.Ease = enumValue1;
			else if (reader.TokenType is JsonTokenType.Number && reader.TryGetInt32(out int intValue1))
				value.Ease = (Global.Components.Easing.EaseType)intValue1;
			else
				value.Ease = default;
		else if (propertyName.SequenceEqual("duration"u8))
			value.Duration = reader.GetSingle();
		else return false;
		return true;
	}
	protected override void Write(Utf8JsonWriter writer, ref SetVFXPreset value, JsonSerializerOptions options)
	{
		base.Write(writer, ref value, options);
		{ writer.WritePropertyName("rooms"u8); JsonSerializer.Serialize(writer, value.Rooms, options); }
		writer.WriteString("preset"u8, value.Preset.ToEnumString());
		if (value.Preset is not VfxPreset.DisableAll)
			writer.WriteBoolean("enable"u8, value.Enable);
		if (value.Enable && value.Preset
			is VfxPreset.Bloom)
			writer.WriteNumber("threshold"u8, value.Threshold);
		if (value.Enable &&
			(SetVFXPreset.EaseVfxs.Contains(value.Preset)
			|| value.Preset is VfxPreset.Diamonds))
			writer.WriteNumber("intensity"u8, value.Intensity);
		if (value.Enable && value.Preset
			is VfxPreset.Bloom
			or VfxPreset.Diamonds
			or VfxPreset.Tutorial
			or VfxPreset.Embers)
		{ writer.WriteString("color"u8, value.Color.Serialize()); }
		if (value.Enable && value.Preset
			is VfxPreset.TileN
			or VfxPreset.CustomScreenScroll)
		{ writer.WritePropertyName("amount"u8); JsonSerializer.Serialize(writer, value.Amount, options); }
		if (value.Enable && value.Preset
			is VfxPreset.WavyRows)
			writer.WriteNumber("speedPerc"u8, value.SpeedPercentage);
		if (value.Enable &&
			SetVFXPreset.EaseVfxs.Contains(value.Preset))
			writer.WriteString("ease"u8, value.Ease.ToEnumString());
		if (value.Enable &&
			SetVFXPreset.EaseVfxs.Contains(value.Preset))
			writer.WriteNumber("duration"u8, value.Duration);
	}
}
