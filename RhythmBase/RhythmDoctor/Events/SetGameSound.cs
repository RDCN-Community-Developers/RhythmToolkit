using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the game sound.
	/// </summary>
	public partial class SetGameSound : BaseEvent, IAudioFileEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetGameSound"/> class.  
		/// </summary>  
		public SetGameSound() { }
		/// <summary>  
		/// Gets or sets the audio associated with the event.  
		/// </summary>  
		private RDAudio Audio { get; set; } = new();
		/// <summary>  
		/// Gets or sets the type of the sound.  
		/// </summary>  
		public SoundTypes SoundType { get; set; } = SoundTypes.SmallMistake;
		/// <summary>  
		/// Gets or sets the filename of the audio.  
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(SoundType)} is not
			(  RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.FreezeshotSound
			or RhythmBase.RhythmDoctor.Components.SoundTypes.BurnshotSound)
			""")]
		public string Filename
		{
			get => Audio.Filename;
			set => Audio.Filename = value;
		}
		/// <summary>  
		/// Gets or sets the volume of the audio.  
		/// </summary>  
		[RDJsonCondition($"""
			$&.{nameof(SoundType)} is not
			(  RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.FreezeshotSound
			or RhythmBase.RhythmDoctor.Components.SoundTypes.BurnshotSound)
			&& $&.{nameof(Volume)} != 100
			""")]
		public int Volume
		{
			get => Audio.Volume;
			set => Audio.Volume = value;
		}
		/// <summary>  
		/// Gets or sets the pitch of the audio.  
		/// </summary>  
		[RDJsonCondition($"""
			$&.{nameof(SoundType)} is not
			(  RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.FreezeshotSound
			or RhythmBase.RhythmDoctor.Components.SoundTypes.BurnshotSound)
			&& $&.{nameof(Pitch)} != 100
			""")]
		public int Pitch
		{
			get => Audio.Pitch;
			set => Audio.Pitch = value;
		}
		/// <summary>  
		/// Gets or sets the pan of the audio.  
		/// </summary>  
		[RDJsonCondition($"""
			$&.{nameof(SoundType)} is not
			(  RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.FreezeshotSound
			or RhythmBase.RhythmDoctor.Components.SoundTypes.BurnshotSound)
			&& $&.{nameof(Pan)} != 0
			""")]
		public int Pan
		{
			get => Audio.Pan;
			set => Audio.Pan = value;
		}
		/// <summary>  
		/// Gets or sets the offset time of the audio.  
		/// </summary>  
		[RDJsonTime("milliseconds")]
		[RDJsonCondition($"""
			$&.{nameof(SoundType)} is not
			(  RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.FreezeshotSound
			or RhythmBase.RhythmDoctor.Components.SoundTypes.BurnshotSound)
			&& $&.{nameof(Offset)} != TimeSpan.Zero
			""")]
		public TimeSpan Offset
		{
			get => Audio.Offset;
			set => Audio.Offset = value;
		}
		/// <summary>  
		/// Gets or sets the list of sound subtypes.  
		/// </summary>  
		[RDJsonCondition($"""
			$&.{nameof(SoundType)}
			is RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.ClapSoundHoldP2
			or RhythmBase.RhythmDoctor.Components.SoundTypes.PulseSoundHold
			or RhythmBase.RhythmDoctor.Components.SoundTypes.PulseSoundHoldP2
			or RhythmBase.RhythmDoctor.Components.SoundTypes.BurnshotSound
			or RhythmBase.RhythmDoctor.Components.SoundTypes.FreezeshotSound
			""")]
		[RDJsonConverter(typeof(SoundSubTypeCollectionConverter))]
		public SoundSubTypeCollection SoundSubtypes { get; set; } = [];
		/// <summary>  
		/// Gets the type of the event.  
		/// </summary>  
		public override EventType Type => EventType.SetGameSound;

		/// <summary>  
		/// Gets the tab associated with the event.  
		/// </summary>  
		public override Tabs Tab => Tabs.Actions;

		IEnumerable<FileReference> IAudioFileEvent.AudioFiles => (Audio.IsFile &&
			SoundType is not SoundTypes.ClapSoundHold
						and not SoundTypes.FreezeshotSound
						and not SoundTypes.BurnshotSound)
						? [Audio.Filename]
						: [];
		IEnumerable<FileReference> IFileEvent.Files => (Audio.IsFile &&
			SoundType is not SoundTypes.ClapSoundHold
						and not SoundTypes.FreezeshotSound
						and not SoundTypes.BurnshotSound)
						? [Audio.Filename]
						: [];

		/// <summary>  
		/// Returns a string that represents the current object.  
		/// </summary>  
		/// <returns>A string that represents the current object.</returns>  
		public override string ToString() => base.ToString() + $" {SoundType}";
	}
}
