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
			return jobj;
		}
	}
}
