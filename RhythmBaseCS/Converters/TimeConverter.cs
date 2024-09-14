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
			object timeType = _timeType;
			bool flag = _timeType == TimeType.Hour;
			if (flag)
			{
				writer.WriteValue(value.TotalHours);
			}
			else
			{
				flag = _timeType == TimeType.Minute;
				if (flag)
				{
					writer.WriteValue(value.TotalMinutes);
				}
				else
				{
					flag = _timeType == TimeType.Second;
					if (flag)
					{
						writer.WriteValue(value.TotalSeconds);
					}
					else
					{
						flag = _timeType == TimeType.MiliSecond;
						if (flag)
						{
							writer.WriteValue(value.TotalMilliseconds);
						}
						else
						{
							flag = _timeType == TimeType.Microsecond;
							if (flag)
							{
								writer.WriteValue(value.TotalMicroseconds);
							}
						}
					}
				}
			}
		}


		public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			float value = JToken.ReadFrom(reader).ToObject<float>();
			return _timeType switch
			{
				TimeType.Hour =>  TimeSpan.FromHours((double)value),
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
