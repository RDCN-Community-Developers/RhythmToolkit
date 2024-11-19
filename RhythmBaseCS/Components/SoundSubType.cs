using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// Subtypes of sound effects.
	/// </summary>
	public class SoundSubType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SoundSubType"/> class.
		/// </summary>
		public SoundSubType()
		{
			Audio = new RDAudio();
		}

		/// <summary>
		/// Gets or sets the referenced audio.
		/// </summary>
		public RDAudio Audio { get; set; }

		/// <summary>
		/// Gets or sets the sound effect name.
		/// </summary>
		public SoundTypes GroupSubtype { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="SoundSubType"/> is used.
		/// </summary>
		public bool Used { get; set; }

		/// <summary>
		/// Gets the filename of the audio.
		/// </summary>
		[JsonProperty]
		public string Filename
		{
			get => Audio.Filename;
			set => Audio.Filename = value;
		}

		/// <summary>
		/// Gets or sets the volume of the audio.
		/// </summary>
		[JsonProperty]
		public int Volume
		{
			get
			{
				return Audio.Volume;
			}
			set
			{
				Audio.Volume = value;
			}
		}

		/// <summary>
		/// Gets or sets the pitch of the audio.
		/// </summary>
		[JsonProperty]
		public int Pitch
		{
			get
			{
				return Audio.Pitch;
			}
			set
			{
				Audio.Pitch = value;
			}
		}

		/// <summary>
		/// Gets or sets the pan of the audio.
		/// </summary>
		[JsonProperty]
		public int Pan
		{
			get
			{
				return Audio.Pan;
			}
			set
			{
				Audio.Pan = value;
			}
		}

		/// <summary>
		/// Gets or sets the offset of the audio.
		/// </summary>
		[JsonConverter(typeof(MilliSecondConverter))]
		public TimeSpan Offset
		{
			get
			{
				return Audio.Offset;
			}
			set
			{
				Audio.Offset = value;
			}
		}

		/// <summary>
		/// Determines whether this instance should be serialized.
		/// </summary>
		/// <returns><c>true</c> if this instance should be serialized; otherwise, <c>false</c>.</returns>
		private bool ShouldSerialize() => Used;

		/// <summary>
		/// Determines whether the filename should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the filename should be serialized; otherwise, <c>false</c>.</returns>
		internal bool ShouldSerializeFilename() => ShouldSerialize();

		/// <summary>
		/// Determines whether the volume should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the volume should be serialized; otherwise, <c>false</c>.</returns>
		internal bool ShouldSerializeVolume() => ShouldSerialize() && Volume != 100;

		/// <summary>
		/// Determines whether the pitch should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the pitch should be serialized; otherwise, <c>false</c>.</returns>
		internal bool ShouldSerializePitch() => ShouldSerialize() && Pitch != 100;

		/// <summary>
		/// Determines whether the pan should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the pan should be serialized; otherwise, <c>false</c>.</returns>
		internal bool ShouldSerializePan() => ShouldSerialize();

		/// <summary>
		/// Determines whether the offset should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the offset should be serialized; otherwise, <c>false</c>.</returns>
		internal bool ShouldSerializeOffset() => ShouldSerialize();
	}
}
