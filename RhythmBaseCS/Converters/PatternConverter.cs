using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Events;

namespace RhythmBase.Converters
{

	internal class PatternConverter : JsonConverter<LimitedList<Patterns>>
	{

		public override void WriteJson(JsonWriter writer, LimitedList<Patterns>? value, JsonSerializer serializer)
		{
			string @out = "";
			IEnumerator<Patterns> enumerator = value!.GetEnumerator();
			while (enumerator.MoveNext())
			{
				switch (enumerator.Current)
				{
					case Patterns.None:
						@out += "-";
						break;
					case Patterns.X:
						@out += "x";
						break;
					case Patterns.Up:
						@out += "u";
						break;
					case Patterns.Down:
						@out += "d";
						break;
					case Patterns.Banana:
						@out += "b";
						break;
					case Patterns.Return:
						@out += "r";
						break;
				}
			}
			writer.WriteValue(@out);
		}


		public override LimitedList<Patterns> ReadJson(JsonReader reader, Type objectType, LimitedList<Patterns>? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			foreach (char c in JToken.ReadFrom(reader).ToObject<string>()!)
			{
				switch (c)
				{
					case 'x':
						existingValue!.Add(Patterns.X);
						break;
					case 'u':
						existingValue!.Add(Patterns.Up);
						break;
					case 'd':
						existingValue!.Add(Patterns.Down);
						break;
					case 'b':
						existingValue!.Add(Patterns.Banana);
						break;
					case 'r':
						existingValue!.Add(Patterns.Return);
						break;
					case '-':
						existingValue!.Add(Patterns.None);
						break;
				}
			}
			return existingValue;
		}
	}
}
