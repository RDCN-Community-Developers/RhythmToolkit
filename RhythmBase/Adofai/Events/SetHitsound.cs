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
/// <summary>
/// 
/// </summary>
[RDJsonEnumSerializable]
public enum HitSound
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
	Hat,
	Kick,
	Shaker,
	Sizzle,
	Chuck,
	ShakerLoud,
	None,
	Hammer,
	KickChroma,
	SnareAcoustic2,
	Sidestick,
	Stick,
	ReverbClack,
	Squareshot,
	PowerDown,
	PowerUp,
	KickHouse,
	KickRupture,
	HatHouse,
	SnareHouse,
	SnareVapor,
	ClapHit,
	ClapHitEcho,
	ReverbClap,
	FireTile,
	IceTile,
	VehiclePositive,
	VehicleNegative
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}