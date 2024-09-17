using System;
using Newtonsoft.Json;
using RhythmBase.Assets;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// Subtypes of sound effects.
	/// </summary>
	public class SoundSubType
	{
		public SoundSubType()
		{
			Audio = new Audio();
		}
		/// <summary>
		/// Referenced audio.
		/// </summary>
		public Audio Audio { get; set; }
		/// <summary>
		/// Sound effect name.
		/// </summary>
		public GroupSubtypes GroupSubtype { get; set; }

		public bool Used { get; set; }

		[JsonProperty]
		private string Filename
		{
			get
			{
				return Audio.Name;
			}
		}

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

		[JsonConverter(typeof(TimeConverter))]
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

		private bool ShouldSerialize() => Used;

		internal bool ShouldSerializeFilename() => ShouldSerialize();

		internal bool ShouldSerializeVolume() => ShouldSerialize() && Volume != 100;

		internal bool ShouldSerializePitch() => ShouldSerialize() && Pitch != 100;

		internal bool ShouldSerializePan() => ShouldSerialize();

		internal bool ShouldSerializeOffset() => ShouldSerialize();
		/// <summary>
		/// Types of sound effects.
		/// </summary>
		public enum GroupSubtypes
		{
			ClapSoundHoldLongEnd,
			ClapSoundHoldLongStart,
			ClapSoundHoldShortEnd,
			ClapSoundHoldShortStart,
			FreezeshotSoundCueLow,
			FreezeshotSoundCueHigh,
			FreezeshotSoundRiser,
			FreezeshotSoundCymbal,
			BurnshotSoundCueLow,
			BurnshotSoundCueHigh,
			BurnshotSoundRiser,
			BurnshotSoundCymbal
		}
	}
}
