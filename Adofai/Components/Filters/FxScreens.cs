namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Screens</b>.
/// </summary>
public struct FxScreens : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Tiles</b>.
	/// </summary>
	[RDJsonProperty("Tiles")]
	public float Tiles { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>color</b>.
	/// </summary>
	[RDJsonProperty("color")]
	public RDColor Color { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Screens";
#else
	public static string Name => "CameraFilterPack_FX_Screens";
#endif
}