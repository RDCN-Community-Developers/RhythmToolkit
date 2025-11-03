namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hexagon Black</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Hexagon_Black")]
public struct FxHexagonBlack : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
}