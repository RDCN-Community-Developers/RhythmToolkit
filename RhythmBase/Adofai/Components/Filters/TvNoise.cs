namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Noise</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Noise")]
[RDJsonObjectSerializable]
public struct TvNoise : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.TvNoise;
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}