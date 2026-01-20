namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Movie</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Movie")]
public struct BlurMovie : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonAlias("Radius")]
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Factor</b>.
	/// </summary>
	[RDJsonAlias("Factor")]
	public float Factor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	[RDJsonAlias("FastFilter")]
	public int FastFilter { get; set; }
}