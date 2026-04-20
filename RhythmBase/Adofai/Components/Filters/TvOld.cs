namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Old</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Old")]
[RDJsonObjectSerializable]
public struct TvOld : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.TvOld;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}