using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to set the game sound.
/// </summary>
public partial class SetGameSound : BaseEvent, IAudioFileEvent
{
	/// <summary>  
	/// Gets or sets the audio associated with the event.  
	/// </summary>  
	private RDAudio Audio { get; set; } = new();
	/// <summary>  
	/// Gets or sets the type of the sound.  
	/// </summary>  
	public SoundType SoundType { get; set; } = SoundType.SmallMistake;
	/// <summary>  
	/// Gets or sets the filename of the audio.  
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(SoundType)} is not
		(  RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.FreezeshotSound)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.BurnshotSound)})
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
		(  RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.FreezeshotSound)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.BurnshotSound)})
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
		(  RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.FreezeshotSound)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.BurnshotSound)})
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
		(  RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.FreezeshotSound)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.BurnshotSound)})
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
		(  RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.FreezeshotSound)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.BurnshotSound)})
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
		is RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.ClapSoundHoldP2)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.PulseSoundHold)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.PulseSoundHoldP2)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.BurnshotSound)}
		or RhythmBase.RhythmDoctor.{nameof(Events)}.{nameof(Events.SoundType)}.{nameof(SoundType.FreezeshotSound)}
		""")]
	[RDJsonConverter(typeof(SoundSubTypeCollectionConverter))]
	public SoundSubTypeCollection SoundSubtypes { get; set; } = [];
	///<inheritdoc/>
	public override EventType Type => EventType.SetGameSound;
	///<inheritdoc/>
	public override Tab Tab => Tab.Sounds;

	IEnumerable<FileReference> IAudioFileEvent.AudioFiles => (Audio.IsFile &&
		SoundType is not SoundType.ClapSoundHold
					and not SoundType.FreezeshotSound
					and not SoundType.BurnshotSound)
					? [Audio.Filename]
					: [];
	IEnumerable<FileReference> IFileEvent.Files => (Audio.IsFile &&
		SoundType is not SoundType.ClapSoundHold
					and not SoundType.FreezeshotSound
					and not SoundType.BurnshotSound)
					? [Audio.Filename]
					: [];

	/// <summary>  
	/// Returns a string that represents the current object.  
	/// </summary>  
	/// <returns>A string that represents the current object.</returns>  
	public override string ToString() => base.ToString() + $" {SoundType}";
}
