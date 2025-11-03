namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV 50</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_50")]
public struct Tv50 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
}