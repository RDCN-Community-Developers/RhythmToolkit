namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Movie</b>.
/// </summary>
public struct BlurMovie : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonProperty("Radius")]
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Factor</b>.
	/// </summary>
	[RDJsonProperty("Factor")]
	public float Factor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	[RDJsonProperty("FastFilter")]
	public int FastFilter { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Movie";
#else
	public static string Name => "CameraFilterPack_Blur_Movie";
#endif
}