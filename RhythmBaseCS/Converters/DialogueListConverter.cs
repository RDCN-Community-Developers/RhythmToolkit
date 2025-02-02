using RhythmBase.Components.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RhythmBase.Converters
{
	internal class DialogueListConverter : JsonConverter<DialogueList>
	{
		public override DialogueList? ReadJson(JsonReader reader, Type objectType, DialogueList? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, DialogueList? value, JsonSerializer serializer) => writer.WriteRawValue(value?.Serialize());
	}
}
