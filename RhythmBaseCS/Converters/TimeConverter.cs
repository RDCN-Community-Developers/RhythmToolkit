using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace RhythmBase.Converters
{
	internal class TimeConverter : JsonConverter<TimeSpan>
	{
		public TimeConverter()
		{
			_timeType = TimeType.MiliSecond;
		}

		public TimeConverter(TimeType type)
		{
			_timeType = type;
		}

		public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
		{
			switch (_timeType)
			{
				case TimeType.Hour:
					writer.WriteValue(value.TotalHours);
					break;
				case TimeType.Minute:
					writer.WriteValue(value.TotalMinutes);
					break;
				case TimeType.Second:
					writer.WriteValue(value.TotalSeconds);
					break;
				case TimeType.MiliSecond:
					writer.WriteValue(value.TotalMilliseconds);
					break;
				case TimeType.Microsecond:
					writer.WriteValue(value.TotalMicroseconds);
					break;
				default:
					break;
			}
		}

		public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			float value = JToken.ReadFrom(reader).ToObject<float>();
			return _timeType switch
			{
				TimeType.Hour => TimeSpan.FromHours((double)value),
				TimeType.Minute => TimeSpan.FromMinutes((double)value),
				TimeType.Second => TimeSpan.FromSeconds((double)value),
				TimeType.MiliSecond => TimeSpan.FromMilliseconds((double)value),
				TimeType.Microsecond => TimeSpan.FromMicroseconds((double)value),
				_ => throw new NotImplementedException()
			};
		}

		private readonly TimeType _timeType;

		public enum TimeType
		{
			Hour,
			Minute,
			Second,
			MiliSecond,
			Microsecond
		}
	}
}
