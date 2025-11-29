using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to play a sound.
	/// </summary>
	public class PlaySound : BaseEvent, IAudioFileEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlaySound"/> class.
		/// </summary>
		public PlaySound() { }
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
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.PlaySound;

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Sounds;

		IEnumerable<FileReference> IAudioFileEvent.AudioFiles => Sound is not null && Sound.IsFile ? [Sound.Filename] : [];
		IEnumerable<FileReference> IFileEvent.Files => Sound is not null && Sound.IsFile ? [Sound.Filename] : [];

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
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
}
