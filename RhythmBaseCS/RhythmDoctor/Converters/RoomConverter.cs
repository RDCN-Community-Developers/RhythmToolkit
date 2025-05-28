using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RoomConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			Type type = value!.GetType();
			if (type == typeof(RDRoom))
			{
				writer.WriteStartArray();
				foreach (int item in ((RDRoom?)value ?? default).Rooms)
					writer.WriteValue(item);
				writer.WriteEndArray();
			}
			else if (type == typeof(RDSingleRoom))
			{
				writer.WriteStartArray();
				writer.WriteValue(((RDSingleRoom?)value ?? default).Value);
				writer.WriteEndArray();
			}
			else
				throw new NotImplementedException();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			JToken token = JArray.Load(reader);
			byte[]? J = token.ToObject<byte[]>();
			if (J == null)
				throw new ConvertingException(token, new Exception($"Unreadable room: \"{J}\". path \"{reader.Path}\""));

			bool flag = objectType == typeof(RDRoom);
			object ReadJson;
			if (flag)
			{
				bool enableTop;
				if (existingValue != null)
				{
					object obj = existingValue;
					enableTop = (obj != null ? (RDRoom)obj : default).EnableTop;
				}
				else
				{
					enableTop = true;
				}
				RDRoom room = new(enableTop);
				foreach (byte item in J)
				{
					room[item] = true;
				}
				ReadJson = room;
			}
			else
			{
				flag = objectType == typeof(RDSingleRoom);
				if (!flag)
				{
					throw new NotImplementedException();
				}
				ReadJson = new RDSingleRoom(J.Single());
			}
			return ReadJson;
		}
		public override bool CanConvert(Type objectType) => objectType == typeof(RDRoom) || objectType == typeof(RDSingleRoom);
	}
}
