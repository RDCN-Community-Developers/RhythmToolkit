namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Halftone</b>.
/// </summary>
public struct DrawingHalftone : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	public float Threshold { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DotSize</b>.
	/// </summary>
	public float DotSize { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Halftone";
#else
	public static string Name => "CameraFilterPack_Drawing_Halftone";
#endif
}