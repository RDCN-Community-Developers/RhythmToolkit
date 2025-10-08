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
			reader.Read();
			while (true)
			{

				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType != JsonTokenType.PropertyName)
				{
					throw new JsonException($"Expected PropertyName token, but got {reader.TokenType}.");
				}
				if (reader.ValueSpan.SequenceEqual("settings"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartObject)
						throw new JsonException($"Expected StartObject token for 'settings', but got {reader.TokenType}.");
					SettingsConverter settingsConverter = new();
					level.Settings = settingsConverter.Read(ref reader, typeof(Settings), options) ?? new();
				}
				else if (reader.ValueSpan.SequenceEqual("rows"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartArray)
						throw new JsonException($"Expected StartArray token for 'rows', but got {reader.TokenType}.");
					RowConverter rowConverter = new();
					while (reader.Read())
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						Row? e = rowConverter.Read(ref reader, typeof(Row), options);
						if (e != null)
							level.Rows.Add(e);
					}
					reader.Read();
				}
				else if (reader.ValueSpan.SequenceEqual("decorations"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartArray)
						throw new JsonException($"Expected StartArray token for 'decorations', but got {reader.TokenType}.");
					DecorationConverter decorationConverter = new();
					while (reader.Read())
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						Decoration? e = decorationConverter.Read(ref reader, typeof(Decoration), options);
						if (e != null)
							level.Decorations.Add(e);
					}
					reader.Read();
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
					List<JsonDocument> maybeIllegalAt = [];
					BaseEventConverter baseEventConverter = new();
					reader.Read();
					while (true)
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						IBaseEvent? e = baseEventConverter.Read(ref reader, typeof(IBaseEvent), options);
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
					reader.Read();
					if (Settings.EnableMacroEvent && maybeDataComment != null && types != null && data != null)
					{
						HashSet<int> matchedIds = [];
						foreach (var mm in maybeMacroEvents)
						{
							if (MacroEvent.TryParse(mm, types, out MacroEvent? result))
							{
								matchedIds.Add(result!.DataId);
								if (result.DataId >= data.Length)
									Settings.HandleUnreadableEvent(JsonSerializer.SerializeToElement(mm, options), $"DataId {result.DataId} is out of range.");
								result._data = data[result.DataId];
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
							Settings.HandleUnreadableEvent(JsonSerializer.SerializeToElement(at, options), $"AdvanceText references non-existent FloatingText id {targetId}.");
						}
					}
				}
				else if (reader.ValueSpan.SequenceEqual("bookmarks"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartArray)
						throw new JsonException($"Expected StartArray token for 'bookmarks', but got {reader.TokenType}.");
					BookmarkConverter bookmarkConverter = new();
					reader.Read();
					while (true)
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						Bookmark? e = bookmarkConverter.Read(ref reader, typeof(Bookmark), options);
						if (e != null)
							level.Bookmarks.Add(e);
						reader.Read();
					}
					reader.Read();
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
						string? e = reader.GetString();
						RDColor color = e is null ? default : RDColor.FromRgba(e);
						level.ColorPalette[colorIndex] = color;
						colorIndex++;
					}
					reader.Read();
				}
				else if (reader.ValueSpan.SequenceEqual("conditionals"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartArray)
						throw new JsonException($"Expected StartArray token for 'conditionals', but got {reader.TokenType}.");
					ConditionalConverter conditionalConverter = new();
					reader.Read();
					while (true)
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						BaseConditional? e = conditionalConverter.Read(ref reader, typeof(BaseConditional), options);
						if (e != null)
							level.Conditionals.Add(e);
					}
					reader.Read();
				}
				else
				{
					reader.Skip();
				}
			}
			level._path = Filepath;
			return level;
		}
		public override void Write(Utf8JsonWriter writer, RDLevel value, JsonSerializerOptions options)
		{
			using MemoryStream stream = new MemoryStream();
			//using Utf8JsonWriter noIndentWriter = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false });
			JsonSerializerOptions localOptions = new(options)
			{
				WriteIndented = false,
			};
			byte[] bytes = GetIndentByte(writer, options.IndentCharacter, 2);
			writer.WriteStartObject();
			writer.WritePropertyName("settings");
			SettingsConverter settingsConverter = new();
			settingsConverter.Write(writer, value.Settings, options);
			writer.WritePropertyName("rows");
			writer.WriteStartArray();
			RowConverter rowConverter = new();
			foreach (Row row in value.Rows)
			{
				stream.SetLength(0);
				if (options.WriteIndented)
					stream.Write(bytes, 0, bytes.Length);
				using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
				rowConverter.Write(noIndentWriter, row, localOptions);
				noIndentWriter.Flush();
				Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
				writer.WriteRawValue(sl);
				noIndentWriter.Reset();
			}
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			DecorationConverter decorationConverter = new();
			foreach (Decoration decoration in value.Decorations)
			{
				stream.SetLength(0);
				if (options.WriteIndented)
					stream.Write(bytes, 0, bytes.Length);
				using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
				decorationConverter.Write(noIndentWriter, decoration, localOptions);
				noIndentWriter.Flush();
				Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
				writer.WriteRawValue(sl);
				noIndentWriter.Reset();
			}
			writer.WriteEndArray();
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			BaseEventConverter baseEventConverter = new();
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
								$"{Utils.Utils.RhythmBaseMacroEventHeader}{EventTypeUtils.MacroTypes.IndexOf(macro.GetType()):X8}{macro.DataId:X8}"
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
					{
						stream.SetLength(0);
						if (options.WriteIndented)
							stream.Write(bytes, 0, bytes.Length);
						using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
						baseEventConverter.Write(noIndentWriter, e, localOptions);
						noIndentWriter.Flush();
						Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
						writer.WriteRawValue(sl);
						noIndentWriter.Reset();
					}
				}
				StringBuilder sb = new() { };
				sb.AppendLine(Utils.Utils.RhythmBaseMacroEventDataHeader);
				sb.AppendLine("# Generated by RhythmBase #");
				for (int i = 0; i < EventTypeUtils.MacroTypes.Count; i++)
				{
					Type t = EventTypeUtils.MacroTypes[i];
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
					stream.SetLength(0);
					if (options.WriteIndented)
						stream.Write(bytes, 0, bytes.Length);
					using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
					baseEventConverter.Write(noIndentWriter, e, localOptions);
					noIndentWriter.Flush();
					Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
					writer.WriteRawValue(sl);
					noIndentWriter.Reset();
				}
			}
			else
			{
				foreach (IBaseEvent e in value)
				{
					if (e is MacroEvent macro)
						throw new ConvertingException("MacroEvent found, but EnableMacroEvent is false in LevelReadOrWriteSettings.");
					else
					{
						stream.SetLength(0);
						if (options.WriteIndented)
							stream.Write(bytes, 0, bytes.Length);
						using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
						baseEventConverter.Write(noIndentWriter, e, localOptions);
						noIndentWriter.Flush();
						Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
						writer.WriteRawValue(sl);
						noIndentWriter.Reset();
					}
				}
			}
			writer.WriteEndArray();
			writer.WritePropertyName("bookmarks");
			writer.WriteStartArray();
			BookmarkConverter bookmarkConverter = new();
			foreach (Bookmark bookmark in value.Bookmarks)
			{
				stream.SetLength(0);
				if (options.WriteIndented)
					stream.Write(bytes, 0, bytes.Length);
				using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
				bookmarkConverter.Write(noIndentWriter, bookmark, localOptions);
				noIndentWriter.Flush();
				Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
				writer.WriteRawValue(sl);
				noIndentWriter.Reset();
			}
			writer.WriteEndArray();
			writer.WritePropertyName("colorPalette");
			writer.WriteStartArray();
			foreach (RDColor color in value.ColorPalette)
				writer.WriteStringValue(color.ToString("RRGGBBAA"));
			writer.WriteEndArray();
			writer.WritePropertyName("conditionals");
			writer.WriteStartArray();
			ConditionalConverter conditionalConverter = new();
			foreach (BaseConditional conditional in value.Conditionals)
			{
				stream.SetLength(0);
				if (options.WriteIndented)
					stream.Write(bytes, 0, bytes.Length);
				using Utf8JsonWriter noIndentWriter = new(stream, new JsonWriterOptions { Indented = false });
				conditionalConverter.Write(noIndentWriter, conditional, localOptions);
				noIndentWriter.Flush();
				Span<byte> sl = stream.GetBuffer().AsSpan(0, (int)stream.Position);
				writer.WriteRawValue(sl);
				noIndentWriter.Reset();
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		private static byte[] GetIndentByte(Utf8JsonWriter writer, char indentChar, int indentSize) => Encoding.UTF8.GetBytes(Environment.NewLine + new string(indentChar, writer.CurrentDepth * 2));
	}
}
