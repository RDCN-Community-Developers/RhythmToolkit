using RhythmBase.Global.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Components.Conditions;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RhythmBase.Global.Extensions.EnumConverter;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class ConditionalConverter : JsonConverter<BaseConditional>
	{
		public override void Write(Utf8JsonWriter writer, BaseConditional? value, JsonSerializerOptions serializer)
		{
			if (value is null)
				return;
			writer.WriteStartObject();
			writer.WriteString("type", value.Type.ToEnumString());
			switch (value.Type)
			{
				case BaseConditional.ConditionType.LastHit:
					writer.WriteNumber("row", ((LastHitCondition)value).Row);
					writer.WriteString("result", ((LastHitCondition)value).Result.ToEnumString());
					break;
				case BaseConditional.ConditionType.Custom:
					writer.WriteString("expression", ((CustomCondition)value).Expression);
					break;
				case BaseConditional.ConditionType.TimesExecuted:
					writer.WriteNumber("time", ((TimesExecutedCondition)value).MaxTimes);
					break;
				case BaseConditional.ConditionType.Language:
					writer.WriteString("language", ((LanguageCondition)value).Language.ToEnumString());
					break;
				case BaseConditional.ConditionType.PlayerMode:
					writer.WriteBoolean("twoPlayerMode", ((PlayerModeCondition)value).TwoPlayerMode);
					break;
				default:
					break;
			}
			writer.WriteEndObject();
		}
		public override BaseConditional? Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions serializer)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException($"Expected StartObject token, but got {reader.TokenType}.");

			ReadOnlySpan<byte> type = default;

			Utf8JsonReader checkpoint = reader;
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					ReadOnlySpan<byte> propertyName = reader.ValueSpan;
					if (propertyName.SequenceEqual("type"u8))
					{
						reader.Read();
						type = reader.ValueSpan;
						if (type.IsEmpty)
							return null;
						break;
					}
					else
					{
						reader.Skip();
					}
				}
			}
			reader = checkpoint;

			if (type.SequenceEqual("Custom"u8))
				return ReadCustom(ref reader, serializer);
			else if (type.SequenceEqual("Language"u8))
				return ReadLanguage(ref reader, serializer);
			else if (type.SequenceEqual("LastHit"u8))
				return ReadLastHit(ref reader, serializer);
			else if (type.SequenceEqual("PlayerMode"u8))
				return ReadPlayerMode(ref reader, serializer);
			else if (type.SequenceEqual("TimeExecuted"u8))
				return ReadTimesExecuted(ref reader, serializer);
			else
				return null;
		}
		private static CustomCondition ReadCustom(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			CustomCondition condition = new();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.ValueSpan;
					reader.Read();
					if (propertyName == "expression"u8)
						condition.Expression = reader.GetString() ?? string.Empty;
				}
			}
			return condition;
		}
		private static LanguageCondition ReadLanguage(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			LanguageCondition condition = new();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.ValueSpan;
					reader.Read();
					if (propertyName == "language"u8 && EnumConverter.TryParse(reader.GetString(), out LanguageCondition.Languages languages))
						condition.Language = languages;
					break;
				}
			}
			return condition;
		}
		private static LastHitCondition ReadLastHit(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			LastHitCondition condition = new();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.ValueSpan;
					reader.Read();
					if (propertyName == "row"u8)
						condition.Row = reader.GetSByte();
					else if (propertyName == "result"u8 && EnumConverter.TryParse(reader.ValueSpan, out LastHitCondition.HitResult result))
						condition.Result = result;
				}
			}
			return condition;
		}
		private static PlayerModeCondition ReadPlayerMode(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			PlayerModeCondition condition = new();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.ValueSpan;
					reader.Read();
					if (propertyName == "twoPlayerMode"u8)
						condition.TwoPlayerMode = reader.GetBoolean();
				}
			}
			return condition;
		}
		private static TimesExecutedCondition ReadTimesExecuted(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			TimesExecutedCondition condition = new();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.ValueSpan;
					reader.Read();
					if (propertyName == "time"u8)
						condition.MaxTimes = reader.GetInt32();
				}
			}
			return condition;
		}
	}
}