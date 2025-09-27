using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Extensions;
using RhythmBase.Global.Converters;
using RhythmBase.Global.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RhythmBase.Adofai.Utils.EventTypeUtils;
namespace RhythmBase.Adofai.Converters
{
	internal class BaseEventConverter : JsonConverter<IBaseEvent>
	{
		//public override void WriteJson(JsonWriter writer, TEvent? value, JsonSerializer serializer)
		//{
		//	if (value == null)
		//		throw new ConvertingException($"Event is null");
		//	serializer.Formatting = Formatting.None;
		//	writer.WriteRawValue(JsonConvert.SerializeObject(SetSerializedObject(value, serializer)));
		//	serializer.Formatting = Formatting.Indented;
		//}
		//public override TEvent? ReadJson(JsonReader reader, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer) => GetDeserializedObject((JObject)JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer);
		//public virtual TEvent? GetDeserializedObject(JObject jobj, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		//{
		//	Type SubClassType = Utils.Utils.ADConvertToType(jobj["eventType"]?.ToObject<string>() ?? throw new IllegalEventTypeException("")) ?? throw new IllegalEventTypeException("SubClassType is null");
		//	_canread = false;
		//	existingValue = (TEvent)(((SubClassType != null) ? jobj.ToObject(SubClassType, serializer) : jobj.ToObject<CustomEvent>(serializer)) ?? throw new IllegalEventTypeException(SubClassType!));
		//	_canread = true;
		//	return existingValue;
		//}
		//public virtual JObject SetSerializedObject(TEvent value, JsonSerializer serializer)
		//{
		//	_canwrite = false;
		//	JObject JObj = JObject.FromObject(value, serializer);
		//	_canwrite = true;
		//	JObj.Remove("type");
		//	JToken? s = JObj.First;
		//	s?.AddBeforeSelf(new JProperty("eventType", value.Type.ToString()));
		//	return JObj;
		//}

		public override IBaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			//if (reader.TokenType != JsonTokenType.StartObject)
			//	throw new JsonException("Expected StartObject token");
			//reader.Read();
			//ReadOnlySpan<byte> type;
			//while (true)
			//{
			//	if (reader.TokenType == JsonTokenType.EndObject)
			//		throw new JsonException("Expected property 'eventType' not found");
			//	if (reader.TokenType != JsonTokenType.PropertyName)
			//		throw new JsonException($"Unexpected token type {reader.TokenType}");
			//	if(reader.ValueSpan.SequenceEqual("eventType"u8))
			//	{
			//		reader.Read();
			//		if (reader.TokenType != JsonTokenType.String)
			//			throw new JsonException("Expected string for 'eventType' property");
			//		type = reader.ValueSpan;
			//		break;
			//	}
			//	reader.Skip();
			//	reader.Read();
			//}


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
				e = ReadForwardEvent(ref reader, typeToConvert, options) ?? new ForwardEvent() { ActureType = type.ToString() ?? "" };
			else
				e = converters[typeEnum].ReadProperties(ref reader, options);
			if (reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException($"Expected EndObject token, but got {reader.TokenType}.");
			reader.Read();
			return e;
		}
		public IForwardEvent? ReadForwardEvent(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			//using var doc = JsonDocument.ParseValue(ref reader);
			//var root = doc.RootElement;

			//// 判断属性
			//bool hasRow = false, hasTarget = false;
			//foreach (var prop in root.EnumerateObject())
			//{
			//	if (prop.NameEquals("row"))
			//		hasRow = true;
			//	else if (prop.NameEquals("target"))
			//		hasTarget = true;
			//}
			//if (hasRow) return new ForwardRowEvent(doc);
			//else if (hasTarget) return new ForwardDecorationEvent(doc);
			//else return new ForwardEvent(doc);
			return new ForwardEvent();
		}

		public override void Write(Utf8JsonWriter writer, IBaseEvent value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}