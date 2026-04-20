namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV WideScreenCircle</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_WideScreenCircle")]
[RDJsonObjectSerializable]
public struct TvWideScreenCircle : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.TvWideScreenCircle;
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