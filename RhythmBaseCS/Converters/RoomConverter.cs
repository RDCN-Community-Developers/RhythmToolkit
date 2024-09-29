using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
namespace RhythmBase.Converters
{
	internal class RoomConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			Type type = value!.GetType();
			if (type == typeof(Room))
			{
				writer.WriteStartArray();
				foreach (int item in (((Room?)value) ?? default).Rooms)
					writer.WriteValue(item);
				writer.WriteEndArray();
			}
			else if (type == typeof(SingleRoom))
			{
				writer.WriteStartArray();
				writer.WriteValue((((SingleRoom?)value) ?? default).Value);
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
				throw new Exceptions.ConvertingException(token, new Exception($"Unreadable room: \"{J}\". path \"{reader.Path}\""));

			bool flag = objectType == typeof(Room);
			object ReadJson;
			if (flag)
			{
				bool enableTop;
				if (existingValue != null)
				{
					object obj = existingValue;
					enableTop = ((obj != null) ? ((Room)obj) : default).EnableTop;
				}
				else
				{
					enableTop = true;
				}
				existingValue = new Room(enableTop);
				foreach (byte item in J)
				{
					NewLateBinding.LateIndexSet(existingValue,
					[
						item,
						true
					], null);
				}
				ReadJson = existingValue;
			}
			else
			{
				flag = objectType == typeof(SingleRoom);
				if (!flag)
				{
					throw new NotImplementedException();
				}
				ReadJson = new SingleRoom(J.Single());
			}
			return ReadJson;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(Room) || objectType == typeof(SingleRoom);
	}
}
