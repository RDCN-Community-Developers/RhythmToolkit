namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Edge filter</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Edge_filter")]
[RDJsonObjectSerializable]
public struct EdgeEdgeFilter : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.EdgeEdgeFilter;
	/// <summary>
	/// Gets or sets the value of the <b>GreenAmplifier</b>.
	/// </summary>
	[RDJsonAlias("GreenAmplifier")]
	public float GreenAmplifier { get; set; }
}