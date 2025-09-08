using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class LevelConverter : JsonConverter<RDLevel>
	{
		internal string? Filepath { get; set; }
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
						while (reader.Read())
						{
							if (reader.TokenType == JsonTokenType.EndArray)
								break;
							IBaseEvent? e = JsonSerializer.Deserialize<IBaseEvent>(ref reader, options);
							if (e == null)
								continue;
							level.Add(e);
							if (e is FloatingText ft)
							{
								floatingTexts[ft["id"].GetInt32()] = ft;
								ft._extraData.Remove("id");
							}
							else if (e is AdvanceText at)
								advanceTexts.Add(at);
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
				writer.WriteRawValue("\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) +
					JsonSerializer.Serialize(row, localOptions));
			}
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			foreach (Decoration decoration in value.Decorations)
			{
				writer.WriteRawValue("\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) + 
					JsonSerializer.Serialize(decoration, localOptions));
			}
			writer.WriteEndArray();
			writer.WritePropertyName("events");
			writer.WriteStartArray();
			foreach (IBaseEvent e in value)
			{
				writer.WriteRawValue("\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) +
					JsonSerializer.Serialize(e, localOptions));
			}
			writer.WriteEndArray();
			writer.WritePropertyName("bookmarks");
			writer.WriteStartArray();
			foreach (Bookmark bookmark in value.Bookmarks)
			{
				writer.WriteRawValue("\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) +
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
				writer.WriteRawValue("\n" + new string(options.IndentCharacter, writer.CurrentDepth * options.IndentSize) +
					JsonSerializer.Serialize(conditional, localOptions));
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
