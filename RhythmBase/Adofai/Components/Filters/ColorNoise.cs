namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Noise</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Noise")]
public struct ColorNoise : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Noise</b>.
	/// </summary>
	[RDJsonAlias("Noise")]
	public float Noise { get; set; }
}