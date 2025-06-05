using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.Global.Settings;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class ReorderSpriteConverter(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<ReorderRow>(level, inputSettings)
	{
		public override ReorderRow? GetDeserializedObject(JObject jobj, Type objectType, ReorderRow? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			jobj["newRoom"] = jobj["newRoom"]?.Value<string>()?[4] ?? '0' - '0';
			return base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
		}
		public override JObject SetSerializedObject(ReorderRow value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			jobj["newRoom"] = $"Room{value.NewRoom.Value}";
			return jobj;
		}
	}
}
