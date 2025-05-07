using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components;/// <summary>
/// Represents an audio file with properties for volume, pitch, pan, and offset.
/// </summary>
public class RDAudio
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RDAudio"/> class with default values.
	/// </summary>
	public RDAudio() { }	/// <summary>
	/// Gets or sets the file name of the audio.
	/// </summary>
	public string Filename { get; set; } = "";	/// <summary>
	/// Gets or sets the volume of the audio.
	/// </summary>
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public int Volume { get; set; } = 100;	/// <summary>
	/// Gets or sets the pitch of the audio.
	/// </summary>
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public int Pitch { get; set; } = 100;	/// <summary>
	/// Gets or sets the pan of the audio.
	/// </summary>
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public int Pan { get; set; }	/// <summary>
	/// Gets or sets the offset of the audio.
	/// </summary>
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	[JsonConverter(typeof(MilliSecondConverter))]
	public TimeSpan Offset { get; set; }	/// <summary>
	/// Gets a value indicating whether the file is a valid audio file based on its extension.
	/// </summary>
	[JsonIgnore]
	public bool IsFile => sourceArray.Contains(Path.GetExtension(Filename));	private static readonly string[] sourceArray =
			[
				".mp3",
				".wav",
				".ogg",
				".aif",
				".aiff"
			];	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString() => Filename;
}
