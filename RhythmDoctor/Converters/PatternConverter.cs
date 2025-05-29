using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class PatternConverter : JsonConverter<Patterns[]>
	{
		public override void WriteJson(JsonWriter writer, Patterns[]? value, JsonSerializer serializer) => writer.WriteValue(Utils.Utils.GetPatternString(value ?? throw new ConvertingException($"Pattern cannot be null.")));

		public override Patterns[] ReadJson(JsonReader reader, Type objectType, Patterns[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string? pattern = JToken.ReadFrom(reader).ToObject<string>();
			if (pattern == null || pattern.Length != 6)
				throw new ConvertingException($"Invalid pattern: {pattern}");
			existingValue ??= new Patterns[6];
			for (int i = 0; i < 6; i++)
			{
				existingValue[i] = pattern[i] switch
				{
					'x' => Patterns.X,
					'u' => Patterns.Up,
					'd' => Patterns.Down,
					'b' => Patterns.Banana,
					'r' => Patterns.Return,
					'-' => Patterns.None,
					_ => throw new ConvertingException($"Invalid pattern character: {pattern[i]}")
				};
			}
			return existingValue;
		}
	}
}
