namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Old</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_Old")]
public struct TvOld : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}