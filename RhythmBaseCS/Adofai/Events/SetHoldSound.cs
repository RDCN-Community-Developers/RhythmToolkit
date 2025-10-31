namespace RhythmBase.Adofai.Events;

/// <summary>  
/// Represents an event that sets the hold sound properties for a tile in the ADOFAI editor.  
/// </summary>  
public class SetHoldSound : BaseTileEvent, ISingleEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.SetHoldSound;
	/// <summary>  
	/// Gets or sets the sound to play at the start of the hold.  
	/// </summary>  
	public HoldSound HoldStartSound { get; set; } = HoldSound.Fuse;
	/// <summary>  
	/// Gets or sets the sound to loop during the hold.  
	/// </summary>  
	public HoldSound HoldLoopSound { get; set; } = HoldSound.Fuse;
	/// <summary>  
	/// Gets or sets the sound to play at the end of the hold.  
	/// </summary>  
	public HoldSound HoldEndSound { get; set; } = HoldSound.Fuse;
	/// <summary>  
	/// Gets or sets the sound to play in the middle of the hold.  
	/// </summary>  
	public HoldMidSound HoldMidSound { get; set; } = HoldMidSound.Fuse;
	/// <summary>  
	/// Gets or sets the type of the mid-hold sound.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(HoldMidSound)} is not RhythmBase.Adofai.Events.{nameof(Events.HoldMidSound)}.{nameof(HoldMidSound.None)}")]
	public HoldMidSoundType HoldMidSoundType { get; set; } = HoldMidSoundType.Once;
	/// <summary>  
	/// Gets or sets the delay before the mid-hold sound is played.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(HoldMidSound)} is not RhythmBase.Adofai.Events.{nameof(Events.HoldMidSound)}.{nameof(HoldMidSound.None)}")]
	public float HoldMidSoundDelay { get; set; } = 0.5f;
	/// <summary>  
	/// Gets or sets the timing reference for the mid-hold sound.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(HoldMidSound)} is not RhythmBase.Adofai.Events.{nameof(Events.HoldMidSound)}.{nameof(HoldMidSound.None)}")]
	public RelativeType HoldMidSoundTimingRelativeTo { get; set; } = RelativeType.End;
	/// <summary>  
	/// Gets or sets the volume of the hold sound.  
	/// </summary>  
	public int HoldSoundVolume { get; set; } = 100;
}
/// <summary>  
/// Represents the types of sounds that can be played at the start or end of a hold.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HoldSound
{
	/// <summary>  
	/// A fuse sound effect.  
	/// </summary>  
	Fuse,
	/// <summary>  
	/// No sound effect.  
	/// </summary>  
	None,
}
/// <summary>  
/// Represents the types of sounds that can be played in the middle of a hold.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HoldMidSound
{
	/// <summary>  
	/// A fuse sound effect.  
	/// </summary>  
	Fuse,
	/// <summary>  
	/// A "SingSing" sound effect.  
	/// </summary>  
	SingSing,
	/// <summary>  
	/// No sound effect.  
	/// </summary>  
	None,
}
/// <summary>  
/// Represents the types of mid-hold sound playback behaviors.  
/// </summary>  
[RDJsonEnumSerializable]
public enum HoldMidSoundType
{
	/// <summary>  
	/// The sound is played once.  
	/// </summary>  
	Once,
	/// <summary>  
	/// The sound is repeated.  
	/// </summary>  
	Repeat,
}
/// <summary>  
/// Represents the timing reference for the mid-hold sound.  
/// </summary>  
[RDJsonEnumSerializable]
public enum RelativeType
{
	/// <summary>  
	/// The timing is relative to the start of the hold.  
	/// </summary>  
	Start,
	/// <summary>  
	/// The timing is relative to the end of the hold.  
	/// </summary>  
	End,
}
