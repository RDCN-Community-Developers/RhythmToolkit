namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Contrast</b>.
/// </summary>
public struct ColorContrast : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Contrast</b>.
	/// </summary>
	public float Contrast { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_Contrast";
#else
	public static string Name => "CameraFilterPack_Color_Contrast";
#endif
}