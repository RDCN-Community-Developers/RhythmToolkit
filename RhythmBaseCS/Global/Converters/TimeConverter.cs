using System.Text.Json;
using System.Text.Json.Serialization;
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
		public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions serializer)
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
				default:
					break;
			}
		}
		public override TimeSpan Read(ref Utf8JsonReader reader, Type objectType,  JsonSerializerOptions serializer)
		{
			return _timeType switch
			{
				TimeType.Hour => TimeSpan.FromHours(reader.GetDouble()),
				TimeType.Minute => TimeSpan.FromMinutes(reader.GetDouble()),
				TimeType.Second => TimeSpan.FromSeconds(reader.GetDouble()),
				TimeType.MiliSecond => TimeSpan.FromMilliseconds(reader.GetInt32()),
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
