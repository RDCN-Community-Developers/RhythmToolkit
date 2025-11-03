namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Edge filter</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Edge_filter")]
public struct EdgeEdgeFilter : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>GreenAmplifier</b>.
	/// </summary>
	[RDJsonProperty("GreenAmplifier")]
	public float GreenAmplifier { get; set; }
}