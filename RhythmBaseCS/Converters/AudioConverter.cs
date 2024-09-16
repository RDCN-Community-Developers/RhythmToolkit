using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Assets;

namespace RhythmBase.Converters
{
	internal class AudioConverter(AssetManager manager) : JsonConverter<Audio>
	{
		private readonly AssetManager manager = manager;
		public override Audio? ReadJson(JsonReader reader, Type objectType, Audio? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			Audio? audio = JToken.ReadFrom(reader).ToObject<Audio?>();
			if (audio != null)
			{
				audio.AudioFile.Manager ??= manager;
				return audio;
			}
			return null;
		}
		public override bool CanWrite => false;
		public override void WriteJson(JsonWriter writer, Audio? value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
