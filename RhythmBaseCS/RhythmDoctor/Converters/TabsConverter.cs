using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Converters
{
	internal class TabsConverter : JsonConverter<Tabs>
	{
		public override Tabs Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.String)
				throw new JsonException($"Expected String token, but got {reader.TokenType}.");
			Tabs tabs = reader.ValueSpan switch
			{
				var v when v.SequenceEqual("Song"u8) => Tabs.Sounds,
				var v when v.SequenceEqual("Sprites"u8) => Tabs.Decorations,
				var v when v.SequenceEqual("Actions"u8) => Tabs.Actions,
				var v when v.SequenceEqual("Rooms"u8) => Tabs.Rooms,
				var v when v.SequenceEqual("Windows"u8) => Tabs.Windows,
				_ => throw new ConvertingException($"Invalid tabs value: '{reader.GetString()}'.")
			};
			return tabs;
		}

		public override void Write(Utf8JsonWriter writer, Tabs value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value switch
			{
				Tabs.Sounds => "Song",
				Tabs.Decorations => "Sprites",
				Tabs.Actions => "Actions",
				Tabs.Rooms => "Rooms",
				Tabs.Windows => "Windows",
				_ => throw new ConvertingException($"Invalid tabs value: '{value}'.")
			});
		}
	}
}
