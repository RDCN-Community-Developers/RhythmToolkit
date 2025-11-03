namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Dot Circle</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Dot_Circle")]
public struct FxDotCircle : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
}