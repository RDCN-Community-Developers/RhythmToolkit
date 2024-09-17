using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Settings;
namespace RhythmBase.Converters
{
	internal class TagActionConverter(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TagAction>(level, inputSettings)
	{
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
