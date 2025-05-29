using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Global.Exceptions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class BookmarkConverter(BeatCalculator? calculator = default) : JsonConverter<Bookmark>
	{
		public override void WriteJson(JsonWriter writer, Bookmark? value, JsonSerializer serializer)
		{
			var beat = value?.Beat.BarBeat ?? throw new ConvertingException("Cannot write the bookmark.");
			writer.WriteStartObject();
			writer.WritePropertyName("bar");
			writer.WriteValue(beat.bar);
			writer.WritePropertyName("beat");
			writer.WriteValue(beat.beat);
			writer.WritePropertyName("color");
			writer.WriteValue((int)value.Color);
			writer.WriteEndObject();
		}
		public override Bookmark ReadJson(JsonReader reader, Type objectType, Bookmark? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken jobj = JToken.ReadFrom(reader);
			uint bar = jobj["bar"]?.ToObject<uint>()
				?? throw new ConvertingException(jobj, new Exception($"Missing property \"{jobj["bar"]}\". path \"{jobj.Path}\""));
			float beat = jobj["beat"]?.ToObject<float>()
				?? throw new ConvertingException(jobj, new Exception($"Missing property \"{jobj["beat"]}\". path \"{jobj.Path}\""));
			return new Bookmark
			{
				Beat = calculator?.BeatOf(bar, beat) ?? new(bar, beat),
#if NETSTANDARD
				Color = (Bookmark.BookmarkColors)Enum.Parse(typeof(Bookmark.BookmarkColors), (string)jobj["color"]!)
#else
				Color = Enum.Parse<Bookmark.BookmarkColors>((string)jobj["color"]!)
#endif
			};
		}
		private readonly BeatCalculator? calculator = calculator;
	}
}
