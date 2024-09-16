using Newtonsoft.Json;
using RhythmBase.Converters;
using System.ComponentModel;

namespace RhythmBase.Assets
{
	/// <summary>
	/// Audio.
	/// </summary>

	public class Audio
	{
		public Audio()
		{
			AudioFile = new Asset<IAudioFile>();
			Volume = 100;
			Pitch = 100;
			Pan = 0;
			Offset = TimeSpan.Zero;
		}
		public Asset<IAudioFile> AudioFile { get; set; }
		/// <summary>
		/// File name.
		/// </summary>
		[JsonProperty("filename")]
		public string Name
		{
			get => AudioFile?.Name ?? "";
			set => AudioFile.Name = value;
		}
		/// <summary>
		/// Audio volume.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		[DefaultValue(100)]
		public int Volume { get; set; }
		/// <summary>
		/// Audio Pitch.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		[DefaultValue(100)]
		public int Pitch { get; set; }
		/// <summary>
		/// Audio Pan.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		public int Pan { get; set; }
		/// <summary>
		/// Audio Offset.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan Offset { get; set; }
		[JsonIgnore]
		public bool IsFile => sourceArray.Contains(Path.GetExtension(Name));

		private static readonly string[] sourceArray =
				[
					".mp3",
					".wav",
					".ogg",
					".aif",
					".aiff"
				];
		/// <inheritdoc/>
		public override string ToString() => Name;
	}
}
