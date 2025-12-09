using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to play a sound.
/// </summary>
public class PlaySound : BaseEvent, IAudioFileEvent
{
	/// <summary>
	/// Gets or sets a value indicating whether the sound is custom.
	/// </summary>
	public bool IsCustom { get; set; } = false;
	/// <summary>
	/// Gets or sets the type of the custom sound.
	/// </summary>
	public CustomSoundTypes CustomSoundType { get; set; } = CustomSoundTypes.CueSound;
	/// <summary>
	/// Gets or sets the audio sound.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Sound)} is not null")]
	public RDAudio? Sound { get; set; } = new() { Filename = "Shaker" };
	///<inheritdoc/>
	public override EventType Type => EventType.PlaySound;
	///<inheritdoc/>
	public override Tab Tab => Tab.Sounds;

	IEnumerable<FileReference> IAudioFileEvent.AudioFiles => Sound is not null && Sound.IsFile ? [Sound.Filename] : [];
	IEnumerable<FileReference> IFileEvent.Files => Sound is not null && Sound.IsFile ? [Sound.Filename] : [];
	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {(IsCustom ? Sound?.ToString() : CustomSoundType.ToString())}";
}
/// <summary>
/// Defines the types of custom sounds.
/// </summary>
[RDJsonEnumSerializable]
public enum CustomSoundTypes
{
	/// <summary>
	/// Cue sound type.
	/// </summary>
	CueSound,
	/// <summary>
	/// Music sound type.
	/// </summary>
	MusicSound,
	/// <summary>
	/// Beat sound type.
	/// </summary>
	BeatSound,
	/// <summary>
	/// Hit sound type.
	/// </summary>
	HitSound,
	/// <summary>
	/// Other sound type.
	/// </summary>
	OtherSound
}
