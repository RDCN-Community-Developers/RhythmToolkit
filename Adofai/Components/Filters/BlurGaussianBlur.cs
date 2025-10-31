namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur GaussianBlur</b>.
/// </summary>
public struct BlurGaussianBlur : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_GaussianBlur";
#else
	public static string Name => "CameraFilterPack_Blur_GaussianBlur";
#endif
}