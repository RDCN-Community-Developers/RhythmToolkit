using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Assets;
using RhythmBase.Components;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Extensions;
using RhythmBase.Settings;
using RhythmBase.Utils;
using SkiaSharp;
namespace RhythmBase.Converters
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
			foreach (RowEventCollection item in value.Rows)
				writer.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			foreach (DecorationEventCollection item2 in value.Decorations)
				writer.WriteRawValue(JsonConvert.SerializeObject(item2, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			foreach (IBaseEvent item3 in ((settings.InactiveEventsHandling == InactiveEventsHandling.Retain) ? value.Where(i => i.Active) : value.AsEnumerable()))
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
			foreach (SKColor item6 in value.ColorPalette)
				writer.WriteRawValue(JsonConvert.SerializeObject(item6, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Close();
		}
		public override RDLevel ReadJson(JsonReader reader, Type objectType, RDLevel? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			HashSet<SpriteFile> assetsCollection = [];
			RDLevel outLevel = new()
			{
				_path = fileLocation
			};
			JsonSerializer AllInOneSerializer = JsonSerializer.Create(outLevel.GetSerializer(settings));
			JArray JEvents = [];
			JArray JBookmarks = [];
			while (reader.Read())
			{
				object name = reader.Value!;
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
						outLevel._rows.AddRange(jarr1.ToObject<List<RowEventCollection>>(AllInOneSerializer)!);
						foreach (RowEventCollection row in outLevel._rows)
						{
							row.Parent = outLevel;
							if (row.Character.IsCustom)
								row.Character.CustomCharacter!.Manager = outLevel.Manager;
						}
						break;
					case "decorations":
						JArray jarr2 = JArray.Load(reader);
						outLevel._decorations.AddRange(jarr2.ToObject<List<DecorationEventCollection>>(AllInOneSerializer)!);
						foreach (DecorationEventCollection deco in outLevel._decorations)
						{
							deco.Parent = outLevel;
							deco._file.Manager = outLevel.Manager;
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
						foreach (SKColor color in jarr4.ToObject<SKColor[]>(AllInOneSerializer)!)
							outLevel.ColorPalette.Add(color);
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
				foreach (JToken item in JEvents)
				{
					if (!(settings.InactiveEventsHandling > InactiveEventsHandling.Retain && (item["active"]?.Value<bool>() ?? false)))
					{
						Type eventType = Utils.Utils.ConvertToType((string)item["type"]!);
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
											FloatingTextCollection.Add(new ValueTuple<FloatingText, int>((FloatingText)TempEvent2, (int)item["id"]!));
											break;
										case EventType.AdvanceText:
											AdvanceTextCollection.Add(new ValueTuple<AdvanceText, int>((AdvanceText)TempEvent2, (int)item["id"]!));
											break;
									}
								}
								if (settings.InactiveEventsHandling == InactiveEventsHandling.Store && !TempEvent2.Active)
									settings.InactiveEvents.Add(TempEvent2);
								else
									outLevel.Add(TempEvent2);
							}
						}
					}
				}
				foreach (var (@event, id) in AdvanceTextCollection)
				{
					FloatingText Parent = FloatingTextCollection.First(((FloatingText @event, int id) i) => i.id == id).@event;
					Parent.Children.Add(@event);
					@event.Parent = Parent;
				}
				outLevel.Bookmarks.AddRange(JBookmarks.ToObject<List<Bookmark>>(AllInOneSerializer)!);
				ReadJson = outLevel;
			}
#warning 没做完
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
