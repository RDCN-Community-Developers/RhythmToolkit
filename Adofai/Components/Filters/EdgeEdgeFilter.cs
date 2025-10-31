namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Edge filter</b>.
/// </summary>
public struct EdgeEdgeFilter : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>GreenAmplifier</b>.
	/// </summary>
	public float GreenAmplifier { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Edge_Edge_filter";
#else
	public static string Name => "CameraFilterPack_Edge_Edge_filter";
#endif
}