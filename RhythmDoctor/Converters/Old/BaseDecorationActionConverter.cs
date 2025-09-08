using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Converters.Old
{
	internal class BaseDecorationActionConverter<TEvent>(RDLevel? level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TEvent>(level, inputSettings) where TEvent : BaseDecorationAction
	{
		public BaseDecorationActionConverter(LevelReadOrWriteSettings inputSettings) : this(null, inputSettings) { }
		public override JObject SetSerializedObject(TEvent value, JsonSerializer serializer)
		{
			JObject jobj = base.SetSerializedObject(value, serializer);
			if (value is Comment comment && comment.Tab is not Tabs.Decorations)
				jobj["y"] = value.Y;
			return jobj;
		}
		public override TEvent? GetDeserializedObject(JObject jobj, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			TEvent? obj = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if (obj is null) return obj;
			if (obj is Comment comment)
				obj.Y = jobj["y"]?.ToObject<int>() ?? 0;
			string decoId = jobj["target"]?.ToObject<string>()!;
			Decoration? Parent = level?.Decorations.FirstOrDefault(i => i.Id == decoId);
			if (Parent == null)
				if (!settings.Linked && obj.Type != EventType.Comment)
					switch (settings.UnreadableEventsHandling)
					{
						case UnreadableEventHandling.Store:
							settings.UnreadableEvents.Add((jobj, $"Cannot find the decoration with id {decoId}."));
							return default;
						case UnreadableEventHandling.ThrowException:
							throw new ConvertingException(string.Format("Cannot find the decoration \"{0}\" at {1}", jobj["target"], obj));
					}
			if (settings.Linked)
				obj._parent = Parent;
			obj._decoId = decoId;
			return obj;
		}
	}
}
