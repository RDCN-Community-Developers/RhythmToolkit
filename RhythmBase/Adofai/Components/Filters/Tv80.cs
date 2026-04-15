namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV 80</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_80")]
[RDJsonObjectSerializable]
public struct Tv80 : IFilter
{
	public FilterType Type => FilterType.Tv80;
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}