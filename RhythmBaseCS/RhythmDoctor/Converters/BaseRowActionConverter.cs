using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class BaseRowActionConverter<TEvent>(RDLevel? level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TEvent>(level, inputSettings) where TEvent : BaseRowAction
	{
		public BaseRowActionConverter(LevelReadOrWriteSettings inputSettings) : this(null, inputSettings) { }
		public override TEvent? GetDeserializedObject(JObject jobj, Type objectType, TEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			TEvent? obj = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			if (obj is null) return obj;
			short rowId = jobj["row"]?.ToObject<short>() ?? throw new ConvertingException("Cannot read the property row.");
			if (rowId == -1)
			{
				if (obj.Type != EventType.TintRows)
				{
					switch (settings.UnreadableEventsHandling)
					{
						case UnreadableEventHandling.Store:
							settings.UnreadableEvents.Add((jobj, $"Cannot find the row with id {rowId}."));
							return default;
						case UnreadableEventHandling.ThrowException:
							throw new ConvertingException($"Cannot find the row \"{jobj["target"]}\" at {obj}");
					}
				}
			}
			else if (settings.Linked && rowId >= level?.Rows.Count)
			{
				switch (settings.UnreadableEventsHandling)
				{
					case UnreadableEventHandling.Store:
						settings.UnreadableEvents.Add((jobj, $"The row id {rowId} out of range."));
						return default;
					case UnreadableEventHandling.ThrowException:
						throw new ConvertingException($"The row id {rowId} out of range.");
				}
			}
			if (settings.Linked && rowId >= 0)
			{
				Row? Parent = level?.Rows[rowId];
				obj._parent = Parent;
			}
			obj._row = rowId;
			return obj;
		}
	}
}
