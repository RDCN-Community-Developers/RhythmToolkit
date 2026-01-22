namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Chromatic Aberration</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Chromatic_Aberration")]
public struct ColorChromaticAberration : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Offset</b>.
	/// </summary>
	[RDJsonAlias("Offset")]
	public float Offset { get; set; }
}