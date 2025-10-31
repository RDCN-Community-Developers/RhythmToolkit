namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Water</b>.
/// </summary>
public struct LightWater : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Alpha</b>.
	/// </summary>
	[RDJsonProperty("Alpha")]
	public float Alpha { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	[RDJsonProperty("Distance")]
	public float Distance { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Light_Water";
#else
	public static string Name => "CameraFilterPack_Light_Water";
#endif
}