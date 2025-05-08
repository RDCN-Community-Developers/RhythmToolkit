using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Adofai.Converters
{
	internal class ADTileReferenceConverter : JsonConverter<TileReference>
	{
		public override TileReference ReadJson(JsonReader reader, Type objectType, TileReference existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JArray array = JArray.Load(reader);
			if (array.Count != 2)
			{
				throw new JsonSerializationException("Invalid TileReference format.");
			}
			int offset = array[0].ToObject<int>();
			RelativeType type = array[1].ToObject<RelativeType>();
			TileReference tileReference = new()
			{
				Offset = offset,
				Type = type
			};
			return tileReference;
		}

		public override void WriteJson(JsonWriter writer, TileReference value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			writer.WriteValue(value.Offset);
			writer.WriteValue(value.Type.ToString());
			writer.WriteEndArray();
		}
	}
}
