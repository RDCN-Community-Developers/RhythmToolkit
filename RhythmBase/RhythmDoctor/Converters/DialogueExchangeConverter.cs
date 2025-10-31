using RhythmBase.Global.Components.RichText;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class DialogueExchangeConverter : JsonConverter<RDDialogueExchange>
	{
		public override RDDialogueExchange? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType != JsonTokenType.String)
				throw new JsonException($"Expected String token, but got {reader.TokenType}.");
			try
			{
				return RDDialogueExchange.Deserialize(reader.GetString() ?? "");
			}
			catch (Exception ex)
			{
				throw new JsonException("Failed to parse dialogue exchange.", ex);
			}
		}

		public override void Write(Utf8JsonWriter writer, RDDialogueExchange value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.Serialize());
		}
	}
}
