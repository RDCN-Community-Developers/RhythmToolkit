//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using RhythmBase.Adofai.Events;

//namespace RhythmBase.Adofai.Converters
//{
//	internal class ValueRangeConverter : JsonConverter<ValueRange>
//	{
//		public override ValueRange ReadJson(JsonReader reader, Type objectType, ValueRange existingValue, bool hasExistingValue, JsonSerializer serializer)
//		{
//			JArray array = JArray.Load(reader);
//			if (array.Count != 2)
//			{
//				throw new JsonSerializationException("Invalid ValueRange format.");
//			}
//			float min = array[0].ToObject<float>();
//			float max = array[1].ToObject<float>();
//			return new ValueRange(min, max);
//		}

//		public override void WriteJson(JsonWriter writer, ValueRange value, JsonSerializer serializer)
//		{
//			writer.WriteStartArray();
//			writer.WriteValue(value.Min);
//			writer.WriteValue(value.Max);
//			writer.WriteEndArray();
//		}
//	}
//	internal class ValueRangePairConverter : JsonConverter<ValueRangePair>
//	{
//		public override ValueRangePair ReadJson(JsonReader reader, Type objectType, ValueRangePair existingValue, bool hasExistingValue, JsonSerializer serializer)
//		{
//			JArray array = JArray.Load(reader);
//			if (array.Count != 2)
//			{
//				throw new JsonSerializationException("Invalid ValueRangePair format.");
//			}
//			ValueRange x = array[0].ToObject<ValueRange>(serializer)!;
//			ValueRange y = array[1].ToObject<ValueRange>(serializer)!;
//			return new ValueRangePair(x, y);
//		}
//		public override void WriteJson(JsonWriter writer, ValueRangePair value, JsonSerializer serializer)
//		{
//			writer.WriteStartArray();
//			writer.WriteStartArray();
//			writer.WriteValue(value.X.Min);
//			writer.WriteValue(value.X.Max);
//			writer.WriteEndArray();
//			writer.WriteStartArray();
//			writer.WriteValue(value.Y.Min);
//			writer.WriteValue(value.Y.Max);
//			writer.WriteEndArray();
//			writer.WriteEndArray();
//		}
//	}
//}
