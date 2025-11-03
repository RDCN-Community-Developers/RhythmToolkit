namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV 80</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_80")]
public struct Tv80 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
}