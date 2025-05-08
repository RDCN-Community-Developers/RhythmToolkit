using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using RhythmBase.Global.Components.RichText;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class DialogueListConverter : JsonConverter<RDDialogueExchange>
	{
		public override RDDialogueExchange? ReadJson(JsonReader reader, Type objectType, RDDialogueExchange? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, RDDialogueExchange? value, JsonSerializer serializer) => writer.WriteRawValue(value?.Serialize());
	}
}
