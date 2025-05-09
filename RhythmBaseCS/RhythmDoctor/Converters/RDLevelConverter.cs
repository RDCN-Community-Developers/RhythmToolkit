using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Components;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Utils;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class RDLevelConverter : JsonConverter<RDLevel>
	{
		public RDLevelConverter(string location, LevelReadOrWriteSettings settings)
		{
			fileLocation = location;
			this.settings = settings;
		}
		public RDLevelConverter(LevelReadOrWriteSettings settings)
		{
			this.settings = settings;
			this.settings.PreloadAssets = false;
		}
		public override void WriteJson(JsonWriter writer, RDLevel? value, JsonSerializer serializer)
		{
			JsonSerializerSettings AllInOneSerializer = value!.GetSerializer(settings);
			writer.Formatting = settings.Indented ? Formatting.Indented : Formatting.None;
			writer.WriteStartObject();
			writer.WritePropertyName("settings");
			writer.WriteRawValue(JsonConvert.SerializeObject(value!.Settings, Formatting.Indented, AllInOneSerializer));
			writer.WritePropertyName("rows");
			writer.WriteStartArray();
			foreach (Row item in value.Rows)
				writer.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			foreach (Decoration item2 in value.Decorations)
				writer.WriteRawValue(JsonConvert.SerializeObject(item2, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			foreach (IBaseEvent item3 in settings.InactiveEventsHandling == InactiveEventsHandling.Retain ? value.Where(i => i.Active) : value)
				if (item3 is Group group)
					if (settings.EnableGroupEvent)
						foreach (var item in group)
						{
							if (item == group)
								throw new RhythmBaseException("A group cannot contain itself as an item.");
							else if (item is SetCrotchetsPerBar)
								throw new RhythmBaseException("SetCrotchetsPerBar events are not allowed within a group.");
							item._beat._calculator = value.Calculator;
							writer.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer));
						}
					else throw new RhythmBaseException("An unexpected error occurred while processing the group event. Ensure that the group event is properly configured and adheres to the expected structure.");
				else
					writer.WriteRawValue(JsonConvert.SerializeObject(item3, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("conditionals");
			writer.WriteStartArray();
			foreach (BaseConditional item4 in value.Conditionals)
				writer.WriteRawValue(JsonConvert.SerializeObject(item4, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("bookmarks");
			writer.WriteStartArray();
			foreach (Bookmark item5 in value.Bookmarks)
				writer.WriteRawValue(JsonConvert.SerializeObject(item5, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("colorPalette");
			writer.WriteStartArray();
			foreach (RDColor item6 in value.ColorPalette)
				writer.WriteRawValue(JsonConvert.SerializeObject(item6, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Close();
		}
		public override RDLevel ReadJson(JsonReader reader, Type objectType, RDLevel? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			RDLevel outLevel = new()
			{
				_path = fileLocation
			};
			JsonSerializer AllInOneSerializer = JsonSerializer.Create(outLevel.GetSerializer(settings));
			JArray JEvents = [];
			JArray JBookmarks = [];
			while (reader.Read())
			{
				string name = (string)reader.Value!;
				reader.Read();
				switch (name)
				{
					case "settings":
						JObject jobj = JObject.Load(reader);
						JToken? Mods = jobj["mods"];
						if (Mods?.Type == JTokenType.String)
							jobj["mods"] = new JArray(Mods);
						outLevel.Settings = jobj.ToObject<Components.Settings>(AllInOneSerializer)!;
						break;
					case "rows":
						JArray jarr1 = JArray.Load(reader);
						outLevel.Rows.AddRange(jarr1.ToObject<List<Row>>(AllInOneSerializer)!);
						foreach (Row row in outLevel.Rows)
						{
							row.Parent = outLevel;
						}
						break;
					case "decorations":
						JArray jarr2 = JArray.Load(reader);
						outLevel.Decorations.AddRange(jarr2.ToObject<List<Decoration>>(AllInOneSerializer)!);
						foreach (Decoration deco in outLevel.Decorations)
						{
							deco.Parent = outLevel;
						}
						break;
					case "conditionals":
						JArray jarr3 = JArray.Load(reader);
						outLevel.Conditionals.AddRange(jarr3.ToObject<List<BaseConditional>>(AllInOneSerializer)!);
						foreach (BaseConditional condi in outLevel.Conditionals)
							condi.ParentCollection = outLevel.Conditionals;
						break;
					case "colorPalette":
						JArray jarr4 = JArray.Load(reader);
						RDColor[] array = jarr4.ToObject<RDColor[]>(AllInOneSerializer) ?? throw new ConvertingException("Cannot read the color palette.");
						if (array.Length == 21)
							for (int i = 0; i < array.Length; i++)
								outLevel.ColorPalette[i] = array[i];
						break;
					case "events":
						JEvents = JArray.Load(reader);
						break;
					case "bookmarks":
						JBookmarks = JArray.Load(reader);
						break;
					default:
						break;
				}
			}
			reader.Close();
			RDLevel ReadJson;
			try
			{
				List<(FloatingText @event, int id)> FloatingTextCollection = [];
				List<(AdvanceText @event, int id)> AdvanceTextCollection = [];
				List<Group> GroupCollection = [];
				foreach (JToken item in JEvents)
				{
					if (!(settings.InactiveEventsHandling > InactiveEventsHandling.Retain && (item["active"]?.Value<bool>() ?? false)))
					{
						Type eventType = EventTypeUtils.ToType((string)item["type"]!);
						if (eventType == null)
						{
							BaseEvent TempEvent;
							if (item["target"] != null)
								TempEvent = item.ToObject<CustomDecorationEvent>(AllInOneSerializer)!;
							else if (item["row"] != null)
								TempEvent = item.ToObject<CustomRowEvent>(AllInOneSerializer)!;
							else
								TempEvent = item.ToObject<CustomEvent>(AllInOneSerializer)!;
							if (settings.InactiveEventsHandling == InactiveEventsHandling.Store && !TempEvent.Active)
								settings.InactiveEvents.Add(TempEvent);
							else
								outLevel.Add(TempEvent);
						}
						else
						{
							BaseEvent TempEvent2 = (BaseEvent)item.ToObject(eventType, AllInOneSerializer)!;
							if (TempEvent2 != null)
							{
								if (TempEvent2.Type != EventType.CustomEvent)
								{
									EventType type = TempEvent2.Type;
									switch (type)
									{
										case EventType.FloatingText:
											FloatingTextCollection.Add(((FloatingText)TempEvent2, (int)item["id"]!));
											break;
										case EventType.AdvanceText:
											AdvanceTextCollection.Add(((AdvanceText)TempEvent2, (int)item["id"]!));
											break;
									}
								}
								if (settings.InactiveEventsHandling == InactiveEventsHandling.Store && !TempEvent2.Active)
									settings.InactiveEvents.Add(TempEvent2);
								else if (settings.EnableGroupEvent && TempEvent2 is Comment comment && Group.TryParse(comment, out Group? group))
								{
									GroupCollection.Add(group);
									outLevel.Add(group);
								}
								else outLevel.Add(TempEvent2);
							}
						}
					}
				}
				if (settings.EnableGroupEvent)
					foreach (var group in GroupCollection)
					{
						outLevel.RemoveAll(i => (
						(i is TagAction tag && Group.MatchTag(tag.ActionTag, out string type, out _, out _)) ||
						(Group.MatchTag(i.Tag, out type, out _, out _))) &&
						type == group.GetType().Name);
					}
				foreach (var (@event, id) in AdvanceTextCollection)
				{
					FloatingText Parent = FloatingTextCollection.First((i) => i.id == id).@event;
					Parent.Children.Add(@event);
					@event.Parent = Parent;
				}
				outLevel.Bookmarks.AddRange(JBookmarks.ToObject<List<Bookmark>>(AllInOneSerializer)!);
				ReadJson = outLevel;
			}
			catch (Exception ex)
			{
				if (outLevel.Settings.Version < 55)
					throw new VersionTooLowException(outLevel.Settings.Version, ex);
				throw new ConvertingException(ex);
			}
			return ReadJson;
		}
		private readonly string fileLocation = "";
		private readonly LevelReadOrWriteSettings settings;
	}
}
