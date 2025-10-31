namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV WideScreenHorizontal</b>.
/// </summary>
public struct TvWideScreenHorizontal : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Smooth</b>.
	/// </summary>
	[RDJsonProperty("Smooth")]
	public float Smooth { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_WideScreenHorizontal";
#else
	public static string Name => "CameraFilterPack_TV_WideScreenHorizontal";
#endif
}