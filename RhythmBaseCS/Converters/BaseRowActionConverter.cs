using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Settings;
namespace RhythmBase.Converters
{
	internal class BaseRowActionConverter<TEvent>(RDLevel level, LevelReadOrWriteSettings inputSettings) : BaseEventConverter<TEvent>(level, inputSettings) where TEvent : BaseRowAction
	{
		public override TEvent GetDeserializedObject(JObject jobj, Type objectType, TEvent existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			TEvent obj = base.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer);
			try
			{
				short rowId = jobj["row"].ToObject<short>();
				bool flag = rowId == -1;
				if (flag)
				{
					bool flag2 = obj.Type != EventType.TintRows;
					if (flag2)
					{
						UnreadableEventHandling unreadableEventsHandling = settings.UnreadableEventsHandling;
						if (unreadableEventsHandling == UnreadableEventHandling.Store)
						{
							settings.UnreadableEvents.Add(jobj);
							return default;
						}
						if (unreadableEventsHandling == UnreadableEventHandling.ThrowException)
						{
							throw new ConvertingException(string.Format("Cannot find the row \"{0}\" at {1}", jobj["target"], obj));
						}
					}
				}
				else
				{
					RowEventCollection Parent = level._rows[(int)rowId];
					obj._parent = Parent;
				}
			}
			catch (Exception ex)
			{
				throw new ConvertingException(string.Format("Cannot find the row {0} at {1}", jobj["row"], obj));
			}
			return obj;
		}
	}
}
