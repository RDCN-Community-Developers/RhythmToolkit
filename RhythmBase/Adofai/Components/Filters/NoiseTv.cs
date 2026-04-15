namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Noise TV</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Noise_TV")]
[RDJsonObjectSerializable]
public struct NoiseTv : IFilter
{
	public FilterType Type => FilterType.NoiseTv;
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}