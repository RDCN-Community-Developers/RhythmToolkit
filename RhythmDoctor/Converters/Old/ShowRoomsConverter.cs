//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using RhythmBase.RhythmDoctor.Components;
//using RhythmBase.RhythmDoctor.Events;

//namespace RhythmBase.RhythmDoctor.Converters.Old
//{
//	internal class ShowRoomsConverter : BaseEventConverter<ShowRooms>
//	{
//		public ShowRoomsConverter(RDLevel? level, LevelReadOrWriteSettings inputSettings) : base(level, inputSettings) { }
//		public ShowRoomsConverter(LevelReadOrWriteSettings inputSettings) : base(inputSettings) { }
//		public override JObject SetSerializedObject(ShowRooms value, JsonSerializer serializer)
//		{
//			JObject ev = base.SetSerializedObject(value, serializer);
//			ev["rooms"] = JArray.FromObject(value.Rooms, serializer);
//			ev["heights"] = JArray.FromObject(value.Height.Heights, serializer);
//			return ev;
//		}
//		public override ShowRooms? GetDeserializedObject(JObject jobj, Type objectType, ShowRooms? existingValue, bool hasExistingValue, JsonSerializer serializer)
//		{
//			ShowRooms ev = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer) ?? new();
//			RDRoom room = jobj["rooms"]?.ToObject<RDRoom>(serializer)
//				?? throw new ConvertingException(jobj, new Exception($"Missing property \"{jobj["rooms"]}\". path \"{jobj.Path}\""));
//			for (int i = 0; i < 4; i++)
//			{
//				ev.Height[i] = new()
//				{
//					IsEnabled = room[(byte)i],
//					Value = jobj["heights"]?[i]?.ToObject<int>(serializer)
//						?? throw new ConvertingException(jobj, new Exception($"Missing property \"{jobj["heights"]?[i]}\". path \"{jobj.Path}\"")),
//				};
//			}
//			return ev;
//		}
//	}
//}
