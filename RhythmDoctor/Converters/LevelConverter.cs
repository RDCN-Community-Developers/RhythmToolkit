using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Utils;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class LevelConverter : JsonConverter<RDLevel>
	{
		internal string? Filepath { get; set; }
		internal LevelReadOrWriteSettings Settings { get; set; } = new();
		public override RDLevel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			RDLevel level = [];
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");
			while (reader.Read())
			{

				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueSpan.SequenceEqual("settings"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartObject)
							throw new JsonException($"Expected StartObject token for 'settings', but got {reader.TokenType}.");
						level.Settings = JsonSerializer.Deserialize<Settings>(ref reader, options) ?? new();
					}
					else if (reader.ValueSpan.SequenceEqual("rows"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartArray)
							throw new JsonException($"Expected StartArray token for 'rows', but got {reader.TokenType}.");
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
								break;
							Row? e = JsonSerializer.Deserialize<Row>(ref reader, options);
							if (e != null)
								level.Rows.Add(e);
						}
					}
					else if (reader.ValueSpan.SequenceEqual("decorations"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartArray)
							throw new JsonException($"Expected StartArray token for 'decorations', but got {reader.TokenType}.");
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
								break;
							Decoration? e = JsonSerializer.Deserialize<Decoration>(ref reader, options);
							if (e != null)
								level.Decorations.Add(e);
						}
					}
					else if (reader.ValueSpan.SequenceEqual("events"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartArray)
							throw new JsonException($"Expected StartArray token for 'events', but got {reader.TokenType}.");
						Dictionary<int, FloatingText> floatingTexts = [];
						List<AdvanceText> advanceTexts = [];
						Dictionary<int, List<IBaseEvent>> maybeGeneratedEvents = [];
						List<TagAction> maybeMacroEvents = [];
						string[]? types = [];
						JsonElement[]? data = [];
						Comment? maybeDataComment = null;
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
								break;
							IBaseEvent? e = JsonSerializer.Deserialize<IBaseEvent>(ref reader, options);
							if (e == null)
								continue;
							if (Settings.EnableMacroEvent)
							{
								if (e is Comment c && MacroEvent.TryGetTypeData(c, out types, out data))
								{
									maybeDataComment ??= c;
									continue;
								}
								else if (e is TagAction ta && MacroEvent.TryMatch(ta))
								{
									maybeMacroEvents.Add(ta);
								}
								else if (e is TagAction ta1 && MacroEvent.MatchTag(ta1.ActionTag, out int type, out _, out _)
									|| MacroEvent.MatchTag(e.Tag, out type, out _, out _))
								{
									if (maybeGeneratedEvents.TryGetValue(type, out var list))
										list.Add(e);
									else
										maybeGeneratedEvents[type] = [e];
								}
								else
									level.Add(e);
							}
							else
								level.Add(e);
							if (e is FloatingText ft)
							{
								floatingTexts[ft["id"].GetInt32()] = ft;
								ft._extraData.Remove("id");
							}
							else if (e is AdvanceText at)
								advanceTexts.Add(at);
						}
						HashSet<int> matchedIds = [];
						foreach (var mm in maybeMacroEvents)
						{
							if (MacroEvent.TryParse(mm, types, out MacroEvent? result))
							{
								matchedIds.Add(result.DataId);
								result._data = data[(
										result.DataId < data.Length ? result.DataId : throw new IndexOutOfRangeException($"DataId {result.DataId} is out of range.")
									)];
								result.Flush();
								level.Add(result);
							}
						}
						foreach (var mms in maybeGeneratedEvents)
						{
							if (!matchedIds.Contains(mms.Key))
								foreach (var e in mms.Value)
									level.Add(e);
						}
						foreach (var at in advanceTexts)
						{
							int targetId = at["id"].GetInt32();
							if (floatingTexts.TryGetValue(targetId, out var ft))
							{
								at.Parent = ft;
								ft.Children.Add(at);
								at._extraData.Remove("id");
							}
							else
							{
								//Console.WriteLine($"Warning: AdvanceText at bar {at.Beat.Bar}, beat {at.Beat.Beat} references non-existent FloatingText id {targetId}.");
							}
						}
					}
					else if (reader.ValueSpan.SequenceEqual("bookmarks"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartArray)
							throw new JsonException($"Expected StartArray token for 'bookmarks', but got {reader.TokenType}.");
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
								break;
							Bookmark? e = JsonSerializer.Deserialize<Bookmark>(ref reader, options);
							if (e != null)
								level.Bookmarks.Add(e);
						}
					}
					else if (reader.ValueSpan.SequenceEqual("colorPalette"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartArray)
							throw new JsonException($"Expected StartArray token for 'colorPalette', but got {reader.TokenType}.");
						int colorIndex = 0;
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
							{
								break;
							}
							string? e = JsonSerializer.Deserialize<string>(ref reader, options);
							RDColor color = e is null ? default : RDColor.FromRgba(e);
							level.ColorPalette[colorIndex] = color;
							colorIndex++;
						}
					}
					else if (reader.ValueSpan.SequenceEqual("conditionals"u8))
					{
						reader.Read();
						if (reader.TokenType != JsonTokenType.StartArray)
							throw new JsonException($"Expected StartArray token for 'conditionals', but got {reader.TokenType}.");
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
								break;
							BaseConditional? e = JsonSerializer.Deserialize<BaseConditional>(ref reader, options);
							if (e != null)
								level.Conditionals.Add(e);
						}
					}
					else
					{
						reader.Skip();
					}
				}
			}
			level._path = Filepath;
			return level;
		}
		public override void Write(Utf8JsonWriter writer, RDLevel value, JsonSerializerOptions options)
		{
			JsonSerializerOptions localOptions = new(options)
			{
				WriteIndented = false,
			};
			writer.WriteStartObject();
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, value.Settings, options);
			writer.WritePropertyName("rows");
			writer.WriteStartArray();
			foreach (Row row in value.Rows)
			{
				writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
					JsonSerializer.Serialize(row, localOptions));
			}
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			foreach (Decoration decoration in value.Decorations)
			{
				writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
					JsonSerializer.Serialize(decoration, localOptions));
			}
			writer.WriteEndArray();
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			if (Settings.EnableMacroEvent)
			{
				JsonSerializerOptions dataOptions = new()
				{
					WriteIndented = false,
				};
				List<MacroEvent> macros = [];
				List<IBaseEvent> normalEvents = [];
				List<string> data = [];
				int id = 0;
				foreach (IBaseEvent e in value)
				{
					if (e is MacroEvent macro)
					{
						macro.Flush();
						string rawData = JsonSerializer.Serialize(macro._data, dataOptions);
						if (!data.Contains(rawData))
						{
							data.Add(rawData);
							macro.DataId = id;
							id++;
							macros.Add(macro);
							foreach (IBaseEvent e2 in macro.GenerateTaggedEvents(
								$"{Utils.Utils.RhythmBaseMacroEventHeader}{EventTypeUtils.GroupTypes.IndexOf(macro.GetType()):X8}{macro.DataId:X8}"
							))
							{
								if (e2 is MacroEvent)
									throw new ConvertingException("MacroEvent cannot contain another MacroEvent.");
								if (e2 is SetCrotchetsPerBar)
									throw new RhythmBaseException("SetCrotchetsPerBar events are not allowed within a MacroEvent.");
								normalEvents.Add(e2);
							}
						}
						normalEvents.Add(macro.GenerateTagAction());
					}
					else
						writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
							JsonSerializer.Serialize(e, localOptions));
				}
				StringBuilder sb = new() { };
				sb.AppendLine(Utils.Utils.RhythmBaseMacroEventDataHeader);
				sb.AppendLine("# Generated by RhythmBase #");
				for (int i = 0; i < EventTypeUtils.GroupTypes.Count; i++)
				{
					Type t = EventTypeUtils.GroupTypes[i];
					sb.AppendLine($"@{t.FullName}");
				}
				for (int i = 0; i < data.Count; i++)
				{
					if (i > 0)
						sb.AppendLine();
					sb.AppendLine(data[i]);
				}
				sb.Replace("\r\n", "\n");
				normalEvents.Add(new Comment() { Y = -1, Text = sb.ToString() });
				foreach (IBaseEvent e in normalEvents)
				{
					writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
						JsonSerializer.Serialize(e, localOptions));
				}
			}
			else
			{
				foreach (IBaseEvent e in value)
				{
					if (e is MacroEvent macro)
						throw new ConvertingException("MacroEvent found, but EnableMacroEvent is false in LevelReadOrWriteSettings.");
					else
						writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
							JsonSerializer.Serialize(e, localOptions));
				}
			}
			writer.WriteEndArray();
			writer.WritePropertyName("bookmarks");
			writer.WriteStartArray();
			foreach (Bookmark bookmark in value.Bookmarks)
			{
				writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
					JsonSerializer.Serialize(bookmark, localOptions));
			}
			writer.WriteEndArray();
			writer.WritePropertyName("colorPalette");
			writer.WriteStartArray();
			foreach (RDColor color in value.ColorPalette)
			{
				JsonSerializer.Serialize(writer, color.ToString("RRGGBBAA"), options);
			}
			writer.WriteEndArray();
			writer.WritePropertyName("conditionals");
			writer.WriteStartArray();
			foreach (BaseConditional conditional in value.Conditionals)
			{
				writer.WriteRawValue((options.WriteIndented ? "\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) : "") +
					JsonSerializer.Serialize(conditional, localOptions));
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
