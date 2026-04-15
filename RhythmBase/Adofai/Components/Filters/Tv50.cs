namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV 50</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_50")]
[RDJsonObjectSerializable]
public struct Tv50 : IFilter
{
	public FilterType Type => FilterType.Tv50;
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}