namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV WideScreenHorizontal</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_WideScreenHorizontal")]
[RDJsonObjectSerializable]
public struct TvWideScreenHorizontal : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.TvWideScreenHorizontal;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Smooth</b>.
	/// </summary>
	[RDJsonAlias("Smooth")]
	public float Smooth { get; set; }
}