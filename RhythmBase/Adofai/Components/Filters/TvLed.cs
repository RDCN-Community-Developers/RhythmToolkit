namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV LED</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_LED")]
[RDJsonObjectSerializable]
public struct TvLed : IFilter
{
	public FilterType Type => FilterType.TvLed;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public int Size { get; set; }
}