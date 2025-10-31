namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Neon</b>.
/// </summary>
public struct EdgeNeon : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>EdgeWeight</b>.
	/// </summary>
	public float EdgeWeight { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Edge_Neon";
#else
	public static string Name => "CameraFilterPack_Edge_Neon";
#endif
}