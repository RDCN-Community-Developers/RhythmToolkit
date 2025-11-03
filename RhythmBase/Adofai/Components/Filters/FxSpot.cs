namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Spot</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Spot")]
public struct FxSpot : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>center</b>.
	/// </summary>
	public RDPointN Center { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonProperty("Radius")]
	public float Radius { get; set; }
}