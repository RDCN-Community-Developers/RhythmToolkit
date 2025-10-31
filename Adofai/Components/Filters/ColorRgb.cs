namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color RGB</b>.
/// </summary>
public struct ColorRgb : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>ColorRGB</b>.
	/// </summary>
	public RDColor ColorRGB { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_RGB";
#else
	public static string Name => "CameraFilterPack_Color_RGB";
#endif
}