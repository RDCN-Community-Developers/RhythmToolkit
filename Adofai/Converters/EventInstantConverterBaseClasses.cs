using RhythmBase.Adofai.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RhythmBase.Adofai.Converters
{
	internal class EventInstantConverterBaseTileEvent<TEvent> : EventInstantConverterBaseEvent<TEvent> where TEvent : BaseTileEvent, new()
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
	internal class EventInstantConverterBaseTaggedTileEvent<TEvent> : EventInstantConverterBaseTileEvent<TEvent> where TEvent : BaseTaggedTileEvent, new()
	{
		protected override bool Read(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, ref TEvent value, JsonSerializerOptions options)
		{
			if (base.Read(ref reader, propertyName, ref value, options))
				return true;
			if (propertyName.SequenceEqual("eventTag"u8))
				value.EventTag = reader.GetString() ?? "";
			else
				return false;
			return true;
		}
	}
}
