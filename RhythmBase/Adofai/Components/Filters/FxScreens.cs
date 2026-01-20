namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Screens</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Screens")]
public struct FxScreens : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Tiles</b>.
	/// </summary>
	[RDJsonAlias("Tiles")]
	public float Tiles { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>color</b>.
	/// </summary>
	public RDColor Color { get; set; }
}