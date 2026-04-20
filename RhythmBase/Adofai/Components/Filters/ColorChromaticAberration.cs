namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Chromatic Aberration</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Chromatic_Aberration")]
[RDJsonObjectSerializable]
public struct ColorChromaticAberration : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.ColorChromaticAberration;
	/// <summary>
	/// Gets or sets the value of the <b>Offset</b>.
	/// </summary>
	[RDJsonAlias("Offset")]
	public float Offset { get; set; }
}