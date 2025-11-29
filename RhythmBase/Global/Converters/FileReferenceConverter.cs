using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmBase.Global.Converters
{
	internal class FileReferenceConverter : JsonConverter<FileReference>
	{
		public override FileReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string? s = reader.GetString();
			if (string.IsNullOrEmpty(s)) return default;
			return new FileReference { Path = s! };
		}

		public override void Write(Utf8JsonWriter writer, FileReference value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.Path);
		}
	}
}
