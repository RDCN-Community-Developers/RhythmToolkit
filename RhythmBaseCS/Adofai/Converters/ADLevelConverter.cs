using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Utils;
using RhythmBase.Extensions;
using RhythmBase.Converters;
using RhythmBase.Settings;
namespace RhythmBase.Adofai.Converters
{
	internal class ADLevelConverter(string location, LevelReadOrWriteSettings settings) : JsonConverter<ADLevel>
	{
		public override void WriteJson(JsonWriter writer, ADLevel? value, JsonSerializer serializer)
		{
			JsonSerializerSettings AllInOneSerializer = new()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Formatting = Formatting.None
			};
			IList<JsonConverter> converters = AllInOneSerializer.Converters;
			converters.Add(new StringEnumConverter());
			converters.Add(new ColorConverter());
			writer.Formatting = settings.Indented ? Formatting.Indented : Formatting.None;
			writer.WriteStartObject();
			writer.WritePropertyName("angleData");
			writer.WriteStartArray();
			foreach (ADTile item in value!)
				writer.WriteRawValue(JsonConvert.SerializeObject(item.Angle, Formatting.None));
			writer.WriteEndArray();
			writer.WritePropertyName("settings");
			writer.WriteRawValue(JsonConvert.SerializeObject(value.Settings, Formatting.Indented, AllInOneSerializer));
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			foreach (ADTile item2 in value)
				writer.WriteRawValue(JsonConvert.SerializeObject(item2, Formatting.None, AllInOneSerializer));
			writer.WriteEndArray();
			writer.WritePropertyName("decorations");
			writer.WriteStartArray();
			foreach (ADBaseEvent item3 in value.Decorations)
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
			JsonSerializer AllInOneSerializer = outLevel.GetSerializer(settings);
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
						//outLevel.AddRange(jobj2.ToObject<List<ADTile>>(AllInOneSerializer)!);
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
			JActions.ToObject<List<ADBaseTileEvent>>(AllInOneSerializer);
			outLevel.Decorations.AddRange(JDecorations.ToObject<List<ADBaseEvent>>(AllInOneSerializer)!);
			return outLevel;
		}
		private readonly string fileLocation = location;
		private readonly LevelReadOrWriteSettings settings = settings;
	}
}
