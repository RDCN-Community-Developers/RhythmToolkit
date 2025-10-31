using RhythmBase.RhythmDoctor.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.RhythmDoctor.Converters
{
	internal class PatternConverter : JsonConverter<Patterns[]>
	{
		public override Patterns[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.String)
				throw new JsonException($"Expected String token, but got {reader.TokenType}.");
			string s = reader.GetString() ?? "";
			reader.Read();
			Patterns[] patterns = new Patterns[s.Length];
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				patterns[i] = c switch
				{
					'x' => Patterns.X,
					'u' => Patterns.Up,
					'd' => Patterns.Down,
					'b' => Patterns.Banana,
					'r' => Patterns.Return,
					'-' => Patterns.None,
					_ => throw new ConvertingException($"Invalid pattern character: '{c}'.")
				};
			}
			return patterns;
		}

		public override void Write(Utf8JsonWriter writer, Patterns[] value, JsonSerializerOptions options)
		{
			StringBuilder sb = new();
			foreach (var pattern in value)
			{
				char c = pattern switch
				{
					Patterns.X => 'x',
					Patterns.Up => 'u',
					Patterns.Down => 'd',
					Patterns.Banana => 'b',
					Patterns.Return => 'r',
					Patterns.None => '-',
					_ => throw new ConvertingException($"Invalid pattern value: '{pattern}'.")
				};
				sb.Append(c);
			}
			writer.WriteStringValue(sb.ToString());
		}
	}
}
