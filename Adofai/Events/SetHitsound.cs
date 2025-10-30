namespace RhythmBase.Adofai.Events;

/// <summary>  
/// Represents an event to set the hitsound in the ADOFAI level.  
/// </summary>  
public class SetHitsound : BaseTileEvent, ISingleEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.SetHitsound;
	/// <summary>  
	/// Gets or sets the game sound associated with the hitsound.  
	/// </summary>  
	public GameSound GameSound { get; set; } = GameSound.Hitsound;
	/// <summary>  
	/// Gets or sets the custom hitsound file.  
	/// </summary>  
	public HitSound Hitsound { get; set; } = HitSound.Kick;
	/// <summary>  
	/// Gets or sets the volume of the hitsound.  
	/// </summary>  
	public int HitsoundVolume { get; set; } = 100;
}
/// <summary>  
/// Represents the predefined game sounds available for hitsounds.  
/// </summary>  
[RDJsonEnumSerializable]
public enum GameSound
{
	/// <summary>  
	/// The default hitsound.  
	/// </summary>  
	Hitsound,
	/// <summary>  
	/// The sound used for midspin events.  
	/// </summary>  
	Midspin
}
[RDJsonEnumSerializable]
public enum HitSound
{
#warning Review the names of these enum members for accuracy.
	None,
	Kick,
}