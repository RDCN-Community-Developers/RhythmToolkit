namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV WideScreenVertical</b>.
/// </summary>
public struct TvWideScreenVertical : IFilter
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
	public readonly string Name => "CameraFilterPack_TV_WideScreenVertical";
#else
	public static string Name => "CameraFilterPack_TV_WideScreenVertical";
#endif
}