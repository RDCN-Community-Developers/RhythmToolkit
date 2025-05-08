using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Utils;
using RhythmBase.Settings;
namespace RhythmBase.Adofai.Converters
{
	internal class LevelConverter : JsonConverter<ADLevel>
	{
		public override void WriteJson(JsonWriter writer, ADLevel? value, JsonSerializer serializer)
		{
			JsonSerializerSettings AllInOneSerializer = value!.GetSerializer(settings);
			writer.Formatting = settings.Indented ? Formatting.Indented : Formatting.None;
			writer.WriteStartObject();
			writer.WritePropertyName("angleData");
			writer.Formatting = Formatting.None;
			writer.WriteStartArray();
			foreach (Tile item in value!)
				writer.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.Formatting = settings.Indented ? Formatting.Indented : Formatting.None;
			writer.WritePropertyName("settings");
			writer.WriteRawValue(JsonConvert.SerializeObject(value.Settings, Formatting.Indented, AllInOneSerializer));
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			foreach (Tile item2 in value)
				foreach (BaseTileEvent item in item2)
					writer.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			foreach (BaseEvent item3 in value.Decorations)
				writer.WriteRawValue(JsonConvert.SerializeObject(item3, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WriteEndObject();
			writer.Close();
		}
		public override ADLevel ReadJson(JsonReader reader, Type objectType, ADLevel? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			ADLevel outLevel = new()
			{
				_path = fileLocation
			};
			JsonSerializer AllInOneSerializer = JsonSerializer.Create(outLevel.GetSerializer(settings));
			JArray JActions = [];
			JArray JDecorations = [];
			while (reader.Read())
			{
				string name = (string)reader.Value!;
				reader.Read();
				object left = name;
				switch (name)
				{
					case "settings":
						JObject jobj = JObject.Load(reader);
						outLevel.Settings = jobj.ToObject<ADSettings>(AllInOneSerializer)!;
						break;
					case "angleData":
						JArray jobj2 = JArray.Load(reader);
						var s = (jobj2.ToObject<List<Tile>>(AllInOneSerializer)!);
						foreach (Tile item in s)
						{
							item.Parent = outLevel;
							outLevel.Add(item);
						}
						break;
					case "actions":
						JActions = JArray.Load(reader);
						break;
					case "decorations":
						JDecorations = JArray.Load(reader);
						break;
				}
			}
			reader.Close();
			JActions.ToObject<List<BaseTileEvent>>(AllInOneSerializer);
			outLevel.Decorations.AddRange(JDecorations.ToObject<List<BaseEvent>>(AllInOneSerializer)!);
			return outLevel;
		}
		private readonly string fileLocation;
		private readonly LevelReadOrWriteSettings settings;

		public LevelConverter(string location, LevelReadOrWriteSettings settings)
		{
			fileLocation = location;
			this.settings = settings;
		}
	}
}
