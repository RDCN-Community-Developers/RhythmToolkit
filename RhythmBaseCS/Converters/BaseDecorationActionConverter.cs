using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Settings;
using System.Diagnostics;
namespace RhythmBase.Converters
{
	internal class BaseDecorationActionConverter<TEvent>(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TEvent>(level, inputSettings) where TEvent : BaseDecorationAction
	{
		public override TEvent? GetDeserializedObject(JObject jobj, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			TEvent? obj = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if (obj is null) return obj;
			string decoId = jobj["target"]?.ToObject<string>()!;
			DecorationEventCollection? Parent = level.ModifiableDecorations.FirstOrDefault(i => i.Id == decoId);
			if (Parent == null && obj.Type != EventType.Comment)
				switch (settings.UnreadableEventsHandling)
				{
					case UnreadableEventHandling.Store:
						settings.UnreadableEvents.Add((jobj,$"Cannot find the decoration with id {decoId}."));
						return default;
					case UnreadableEventHandling.ThrowException:
#if DEBUG
						Debugger.Log(2, "cate", "");
						return default;
#else
						throw new ConvertingException(string.Format("Cannot find the decoration \"{0}\" at {1}", jobj["target"], obj));
#endif
				}
			obj._parent = Parent!;
			return obj;
		}
	}
}
