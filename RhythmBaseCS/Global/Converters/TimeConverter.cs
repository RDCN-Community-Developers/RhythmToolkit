using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace RhythmBase.Global.Converters
{
	internal abstract class TimeConverter : JsonConverter<TimeSpan>
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
					writer.WriteRawValue(value.TotalHours.ToString());
					break;
				case TimeType.Minute:
					writer.WriteRawValue(value.TotalMinutes.ToString());
					break;
				case TimeType.Second:
					writer.WriteRawValue(value.TotalSeconds.ToString());
					break;
				case TimeType.MiliSecond:
					writer.WriteRawValue(((int)value.TotalMilliseconds).ToString());
					break;
				case TimeType.Microsecond:
					writer.WriteRawValue(((int)value.TotalMicroseconds).ToString());
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
				TimeType.MiliSecond => TimeSpan.FromMilliseconds((int)value),
				TimeType.Microsecond => TimeSpan.FromMicroseconds((int)value),
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
