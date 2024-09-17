using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Events;
namespace RhythmBase.Converters
{
	internal class TabsConverter : JsonConverter<Tabs>
	{
		public override void WriteJson(JsonWriter writer, Tabs value, JsonSerializer serializer) => writer.WriteValue(TabNames[(int)value]);

		public override Tabs ReadJson(JsonReader reader, Type objectType, Tabs existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			string value = JToken.Load(reader).ToObject<string>();
			int t = TabNames.ToList().IndexOf(value);
			bool flag = t >= 0;
			Tabs ReadJson;
			if (flag)
			{
				ReadJson = (Tabs)t;
			}
			else
			{
				ReadJson = Tabs.Unknown;
			}
			return ReadJson;
		}

		private static readonly string[] TabNames =
		[
			"Song",
			"Rows",
			"Actions",
			"Sprites",
			"Rooms"
		];
	}
}
