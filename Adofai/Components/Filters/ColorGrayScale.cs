namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color GrayScale</b>.
/// </summary>
public struct ColorGrayScale : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonProperty("_Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_GrayScale";
#else
	public static string Name => "CameraFilterPack_Color_GrayScale";
#endif
}