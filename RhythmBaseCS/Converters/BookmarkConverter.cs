﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Utils;
namespace RhythmBase.Converters
{
	internal class BookmarkConverter(BeatCalculator calculator) : JsonConverter<Bookmark>
	{
		public override void WriteJson(JsonWriter writer, Bookmark value, JsonSerializer serializer)
		{
			ValueTuple<uint, float> beat = value.Beat.BarBeat;
			writer.WriteStartObject();
			writer.WritePropertyName("bar");
			writer.WriteValue(beat.Item1);
			writer.WritePropertyName("beat");
			writer.WriteValue(beat.Item2);
			writer.WritePropertyName("color");
			writer.WriteValue((int)value.Color);
			writer.WriteEndObject();
		}

		public override Bookmark ReadJson(JsonReader reader, Type objectType, Bookmark existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken jobj = JToken.ReadFrom(reader);
			return new Bookmark
			{
				Beat = calculator.BeatOf(jobj["bar"].ToObject<uint>(), jobj["beat"].ToObject<float>()),
				Color = Enum.Parse<Bookmark.BookmarkColors>((string)jobj["color"])
			};
		}

		private readonly BeatCalculator calculator = calculator;
	}
}