using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Components;
using System.Text.RegularExpressions;
namespace RhythmBase.Converters
{
	internal partial class ConditionConverter(List<BaseConditional> Conditionals) : JsonConverter<Condition>
	{
		public override void WriteJson(JsonWriter writer, Condition? value, JsonSerializer serializer) => writer.WriteValue(value!.Serialize());
		public override Condition ReadJson(JsonReader reader, Type objectType, Condition? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string s = JToken.Load(reader).ToObject<string>()!;
			Condition Value = new();
			int p = 0;
			do
			{
				bool active = ReadActive(s, ref p);
				int index = ReadInt(s, ref p) - 1;
				if (index < conditionals.Count)
				{
					Value.ConditionLists.Add((active, conditionals[index]));
				}
			}
			while (!ReadIsEndList(s, ref p));
			Value.Duration = ReadFloat(s, ref p);
			return Value;
		}
		private static int ReadInt(string s, ref int p)
		{
			int r = 0;
			while (p < s.Length && char.IsAsciiDigit(s[p]))
			{
				r *= 10;
				r += s[p] - '0';
				p++;
			}
			return r;
		}
		private static float ReadFloat(string s, ref int p)
		{
			float r = ReadInt(s, ref p);
			if (p < s.Length && s[p] == '.')
			{
				p++;
				float f = 0.1f;
				while (p < s.Length && char.IsAsciiDigit(s[p]))
				{
					r += f * (s[p] - '0');
					f *= 0.1f;
					p++;
				}
			}
			return r;
		}
		private static bool ReadActive(string s, ref int p)
		{
			if (p >= s.Length || s[p] == '~')
			{
				p++;
				return false;
			}
			return true;
		}
		private static bool ReadIsEndList(string s, ref int p) => p < s.Length && s[p++] == 'd';
		private readonly List<BaseConditional> conditionals = Conditionals;
	}
}
