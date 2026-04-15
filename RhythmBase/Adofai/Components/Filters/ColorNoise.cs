namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Noise</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Noise")]
[RDJsonObjectSerializable]
public struct ColorNoise : IFilter
{
	public FilterType Type => FilterType.ColorNoise;
	/// <summary>
	/// Gets or sets the value of the <b>Noise</b>.
	/// </summary>
	[RDJsonAlias("Noise")]
	public float Noise { get; set; }
}