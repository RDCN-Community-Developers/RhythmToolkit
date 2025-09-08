/* 项目“RhythmBase (net8.0)”的未合并的更改
在此之前:
using RhythmBase.RhythmDoctor.Events;
在此之后:
using RhythmBase.RhythmDoctor;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Converters.Events;
using RhythmBase.RhythmDoctor.Events;
*/
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal abstract class EventInstantConverterBaseRowAction<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : BaseRowAction, new()
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
			writer.WriteNumber("row"u8, value._row);
		}
	}
	internal abstract class EventInstantConverterBaseBeat<TEvent> : EventInstantConverterBaseRowAction<TEvent> where TEvent : BaseBeat, new()
	{
	}
	//internal class EventInstantConverterAddClassicBeat : EventInstantConverterBaseBeat<AddClassicBeat>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref AddClassicBeat value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//		if (propertyName.SequenceEqual("tick"u8))
	//			value.Tick = reader.GetSingle();
	//		else if (propertyName.SequenceEqual("swing"u8))
	//			value.Swing = reader.GetSingle();
	//		else if (propertyName.SequenceEqual("hold"u8))
	//			value.Hold = reader.GetSingle();
	//		else if (propertyName.SequenceEqual("length"u8))
	//			value.Length = reader.GetUInt16();
	//		else if (propertyName == "setXs"u8 && TryParse(reader.ValueSpan, out ClassicBeatPatterns pattern))
	//			value.SetXs = pattern;
	//	}

	//	protected override void Write(Utf8JsonWriter writer, ref AddClassicBeat value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//		writer.WriteNumber("tick"u8, value.Tick);
	//		writer.WriteNumber("swing"u8, value.Swing);
	//		writer.WriteNumber("hold"u8, value.Hold);
	//		writer.WriteNumber("length"u8, value.Length);
	//		writer.WriteString("setXs"u8, value.SetXs?.ToEnumString());
	//	}
	//}
	internal abstract class EventInstantConverterBaseDecorationAction<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : BaseDecorationAction, new()
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
			if (value is not Comment cmt || cmt.CustomTab == Tabs.Decorations)
				writer.WriteString("target"u8, value._decoId);
		}
	}
	internal abstract class EventInstantConverterBaseRowAnimation<TEvent> : EventInstantConverterBaseRowAction<TEvent> where TEvent : BaseRowAnimation, new()
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
	internal abstract class EventInstantConverterBaseBeatsPerMinute<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : BaseBeatsPerMinute, new()
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
	internal abstract class EventInstantConverterBaseWindowEvent<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : BaseWindowEvent, new()
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
	//internal class EventInstantConverterAddOneshotBeat : EventInstantConverterBaseBeat<AddOneshotBeat>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref AddOneshotBeat value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//		if (propertyName == "pulseType"u8 && TryParse(propertyName, out OneshotPulseShapeTypes result))
	//			value.PulseType = result;
	//		else if (propertyName.SequenceEqual("subdivisions"u8))
	//			value.Subdivisions = reader.GetByte();
	//		else if (propertyName.SequenceEqual("subdivSound"u8))
	//			value.SubdivSound = reader.GetBoolean();
	//		else if (propertyName.SequenceEqual("tick"u8))
	//			value.Tick = reader.GetSingle();
	//		else if (propertyName.SequenceEqual("loops"u8))
	//			value.Loops = reader.GetUInt32();
	//		else if (propertyName.SequenceEqual("internal"u8))
	//			value.Interval = reader.GetSingle();
	//		else if (propertyName.SequenceEqual("skipshot"u8))
	//			value.Skipshot = reader.GetBoolean();
	//		else if (propertyName.SequenceEqual("hold"u8))
	//			value.Hold = reader.GetBoolean();
	//		else if (propertyName.SequenceEqual("suddenHoldCue"u8))
	//			value.SuddenHoldCue = reader.GetBoolean();
	//		else if (propertyName == "freezeBurnMode"u8 && EnumConverter.TryParse(propertyName, out OneshotTypes freezeBurnMode))
	//			value.FreezeBurnMode = freezeBurnMode;
	//		else if (propertyName.SequenceEqual("delay"u8))
	//			value.Delay = reader.GetSingle();
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref AddOneshotBeat value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//		writer.WriteBoolean("hold"u8, value.Hold);
	//	}
	//}
	//internal class EventInstantConverterCustomDecorationEvent : EventInstantConverterBaseDecorationAction<CustomDecorationEvent>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref CustomDecorationEvent value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref CustomDecorationEvent value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
	//internal class EventInstantConverterCustomEvent : EventInstantConverterBaseEvent<CustomEvent>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref CustomEvent value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref CustomEvent value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
	//internal class EventInstantConverterCustomRowEvent : EventInstantConverterBaseRowAction<CustomRowEvent>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref CustomRowEvent value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref CustomRowEvent value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
	//internal class EventInstantConverterFloatingText : EventInstantConverterBaseEvent<FloatingText>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref FloatingText value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref FloatingText value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
	//internal class EventInstantConverterSetCountingSound : EventInstantConverterBaseRowAction<SetCountingSound>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref SetCountingSound value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref SetCountingSound value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
	//internal class EventInstantConverterSetRoomPerspective : EventInstantConverterBaseEvent<SetRoomPerspective>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref SetRoomPerspective value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref SetRoomPerspective value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
	//internal class EventInstantConverterSetRowXs : EventInstantConverterBaseBeat<SetRowXs>
	//{
	//	protected override void Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref SetRowXs value, JsonSerializerOptions options)
	//	{
	//		base.Read(ref reader, propertyName, ref value, options);
	//	}
	//	protected override void Write(Utf8JsonWriter writer, ref SetRowXs value, JsonSerializerOptions options)
	//	{
	//		base.Write(writer, ref value, options);
	//	}
	//}
}
