using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.Adofai.Converters
{
	internal class LevelConverter : JsonConverter<ADLevel>
	{
		internal string? Filepath { get; set; }
		internal LevelReadOrWriteSettings Settings { get; set; } = new();

		public override ADLevel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ADLevel level = [];
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");
			reader.Read();
			while (true)
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType != JsonTokenType.PropertyName)
					throw new JsonException($"Expected PropertyName token, but got {reader.TokenType}.");
				if (reader.ValueSpan.SequenceEqual("angleData"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartArray)
						throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
					reader.Read();
					while (true)
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						if(reader.TokenType != JsonTokenType.Number)
							throw new JsonException($"Expected Number token, but got {reader.TokenType}.");
						int angle = reader.GetInt32();
						if (angle == Utils.Utils.MidSpinAngle)
							level.Add(new(true));
						else
							level.Add(new(angle));
						reader.Read();
					}
					reader.Read();
				}
				else if (reader.ValueSpan.SequenceEqual("settings"u8))
				{
					reader.Read();
					SettingsConverter settingsConverter = new();
					level.Settings = settingsConverter.Read(ref reader, typeof(Settings), options) ?? new();
				}
				else if(reader.ValueSpan.SequenceEqual("actions"u8))
				{

				}
				else if(reader.ValueSpan.SequenceEqual("decoration"u8))
				{
					reader.Read();
					if (reader.TokenType != JsonTokenType.StartArray)
						throw new JsonException($"Expected StartArray token, but got {reader.TokenType}.");
					reader.Read();
					BaseEventConverter baseEventConverter = new();
					while (true)
					{
						if (reader.TokenType == JsonTokenType.EndArray)
							break;
						IBaseEvent? e = baseEventConverter.Read(ref reader, typeof(BaseEvent), options) as BaseEvent;
						if (e != null)
							level.Decorations.Add(e);
					}
					reader.Read();
				}
				else
				{
					reader.Skip();
					reader.Read();
				}
			}
			reader.Read();
			return level;
		}

		public override void Write(Utf8JsonWriter writer, ADLevel value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
