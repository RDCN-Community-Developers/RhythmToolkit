using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;

namespace RhythmBase.Converters
{
	internal class RoomOrderConverter : JsonConverter<RoomOrder>
	{
		public override RoomOrder ReadJson(JsonReader reader, Type objectType, RoomOrder existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			byte[]? order = JToken.ReadFrom(reader).ToObject<byte[]>();
			ArgumentNullException.ThrowIfNull(order);
			if (order.Length != 4)
				throw new Exceptions.ConvertingException($"Invalid room order: {string.Join(",", order)}");
			return new RoomOrder(order[0], order[1], order[2], order[3]);
		}

		public override void WriteJson(JsonWriter writer, RoomOrder value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			byte[] order = value.Order;
			for (int i = 0; i < 4; i++)
				writer.WriteValue(order[i]);
			writer.WriteEndArray();
		}
	}
}
