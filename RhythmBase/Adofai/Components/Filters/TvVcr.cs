namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Vcr</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Vcr")]
[RDJsonObjectSerializable]
public struct TvVcr : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.TvVcr;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}