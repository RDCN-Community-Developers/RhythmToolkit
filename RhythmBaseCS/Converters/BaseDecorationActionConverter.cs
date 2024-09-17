using System;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Settings;
namespace RhythmBase.Converters
{
	internal class BaseDecorationActionConverter<TEvent>(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TEvent>(level, inputSettings) where TEvent : BaseDecorationAction
	{
		public override TEvent GetDeserializedObject(JObject jobj, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			TEvent obj = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			string decoId = jobj["target"]?.ToObject<string>()!;
			DecorationEventCollection? Parent = level._decorations.FirstOrDefault(i => i.Id == decoId);
			if (Parent == null && obj.Type != EventType.Comment)
				switch (settings.UnreadableEventsHandling)
				{
					case UnreadableEventHandling.Store:
						settings.UnreadableEvents.Add(jobj);
						return default;
					case UnreadableEventHandling.ThrowException:
						throw new ConvertingException(string.Format("Cannot find the decoration \"{0}\" at {1}", jobj["target"], obj));
				}
						obj._parent = Parent!;
			return obj;
		}
	}
}
