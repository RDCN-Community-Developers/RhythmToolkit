using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using System.Runtime.CompilerServices;

namespace RhythmBase.Converters
{

	internal class LimitedListConverter : JsonConverter<LimitedList>
	{

		public override void WriteJson(JsonWriter writer, LimitedList? value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			foreach (object obj in value!)
			{
				object item = RuntimeHelpers.GetObjectValue(obj);
				serializer.Serialize(writer, RuntimeHelpers.GetObjectValue(item));
			}
			writer.WriteEndArray();
		}


		public override LimitedList ReadJson(JsonReader reader, Type objectType, LimitedList? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JArray J = JArray.Load(reader);
			existingValue ??= new LimitedList<object>(checked((uint)J.Count));
			existingValue.Clear();
			foreach (JToken item in J)
				existingValue.Add(item.ToObject<object>(serializer)!);
			return existingValue;
		}
	}
}
