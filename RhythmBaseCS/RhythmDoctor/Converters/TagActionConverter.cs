using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class TagActionConverter(RDLevel? level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TagAction>(level, inputSettings)
	{
		public TagActionConverter(LevelReadOrWriteSettings inputSettings) : this(null, inputSettings) { }
		public override TagAction? GetDeserializedObject(JObject jobj, Type objectType, TagAction? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			TagAction? ev = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if (ev is not null)
			{
				string action = jobj["Action"]?.ToObject<string>() ?? "Run";
				ev.Action = (
					action.StartsWith("Run") ? TagAction.Actions.Run :
					action.StartsWith("Enable") ? TagAction.Actions.Enable :
					action.StartsWith("Disable") ? TagAction.Actions.Disable : 0) |
					(action.EndsWith("All") ? TagAction.Actions.All : 0);
			}
			return ev;
		}
		public override JObject SetSerializedObject(TagAction value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			if (value.Tag == null)
			{
				jobj.Remove("tag");
			}
			else
			{
				jobj["tag"] = value.Tag;
			}
			jobj["Tag"] = value.ActionTag;
			jobj["Action"] = (value.Action & (TagAction.Actions)0b110).ToString() + (value.Action.HasFlag(TagAction.Actions.All) ? "All" : "");
			return jobj;
		}
	}
}
