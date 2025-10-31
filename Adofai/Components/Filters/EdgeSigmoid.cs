namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Sigmoid</b>.
/// </summary>
public struct EdgeSigmoid : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Gain</b>.
	/// </summary>
	[RDJsonProperty("Gain")]
	public float Gain { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Edge_Sigmoid";
#else
	public static string Name => "CameraFilterPack_Edge_Sigmoid";
#endif
}