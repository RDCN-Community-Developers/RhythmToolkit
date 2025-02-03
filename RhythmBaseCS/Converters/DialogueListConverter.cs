using RhythmBase.Components.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RhythmBase.Converters
{
	internal class DialogueListConverter : JsonConverter<RDDialogueList>
	{
		public override RDDialogueList? ReadJson(JsonReader reader, Type objectType, RDDialogueList? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, RDDialogueList? value, JsonSerializer serializer) => writer.WriteRawValue(value?.Serialize());
	}
}
