using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RhythmBase.RhythmDoctor.Utils.EventTypeUtils;

namespace RhythmBase.RhythmDoctor.Converters.Events
{
	internal class BaseEventConverter : JsonConverter<IBaseEvent>
	{
		private delegate void PropertyAction(ref Utf8JsonReader reader);
		public override bool CanConvert(Type typeToConvert)
		{
			return Type.IsAssignableFrom(typeToConvert);
		}
		public override IBaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");

			ReadOnlySpan<byte> type = default;

			Utf8JsonReader checkpoint = reader;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueSpan.SequenceEqual("type"u8))
					{
						reader.Read();
						type = reader.ValueSpan;
						break;
					}
					else
					{
						reader.Skip();
					}
				}
			}
			reader = checkpoint; IBaseEvent e;
			if (!EnumConverter.TryParse(type, out EventType typeEnum))
				e = ReadCustomEvent(ref reader, typeToConvert, options) ?? new CustomEvent() { ActureType = type.ToString() ?? "" };
			else
				e = converters[typeEnum].ReadProperties(ref reader, options);
			return e;
		}
		public override void Write(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options)
		{
			if (value is ICustomEvent ce)
			{
				WriteCustomEvent(writer, ce, options);
				return;
			}
			else
			{
				converters[value.Type].WriteProperties(writer, value, options);
			}
		}
		public ICustomEvent? ReadCustomEvent(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using var doc = JsonDocument.ParseValue(ref reader);
			var root = doc.RootElement;

			// 判断属性
			bool hasRow = false, hasTarget = false;
			foreach (var prop in root.EnumerateObject())
			{
				if (prop.NameEquals("row"))
					hasRow = true;
				else if (prop.NameEquals("target"))
					hasTarget = true;
			}
			// 默认 CustomEvent
			ICustomEvent ce = new CustomEvent();

			if (hasRow)
				ce = new CustomRowEvent();
			if (hasTarget)
				ce = new CustomDecorationEvent();

			foreach (var prop in root.EnumerateObject())
			{
				switch (prop.Name)
				{
					case "type": ce.ActureType = prop.Value.GetString() ?? ""; break;
					case "bar": ce.Bar = prop.Value.GetInt32(); break;
					case "beat": ce.BeatValue = prop.Value.GetSingle(); break;
					case "tag": ce.Tag = prop.Value.GetString() ?? ""; break;
					case "active": ce.Active = prop.Value.GetBoolean(); break;
					case "condition": ce.ConditionRaw = prop.Value.GetString(); break;
					case "y": ce.Y = prop.Value.GetInt32(); break;
					default: ce.ExtraData[prop.Name] = prop.Value.Clone(); break;
				}
			}
			return ce;
		}

		public void WriteCustomEvent(Utf8JsonWriter writer, ICustomEvent value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value.ActureType))
				writer.WriteString("type", value.ActureType);
			writer.WriteNumber("bar", value.Bar);
			writer.WriteNumber("beat", value.BeatValue);
			if (!string.IsNullOrEmpty(value.Tag))
				writer.WriteString("tag", value.Tag);
			if (!value.Active)
				writer.WriteBoolean("active", value.Active);
			if (!string.IsNullOrEmpty(value.ConditionRaw))
				writer.WriteString("condition", value.ConditionRaw);
			if (value.Y != 0)
				writer.WriteNumber("y", value.Y);

			foreach (var kv in value.ExtraData)
			{
				writer.WritePropertyName(kv.Key);
				kv.Value.WriteTo(writer);
			}
			writer.WriteEndObject();
		}
	}
}