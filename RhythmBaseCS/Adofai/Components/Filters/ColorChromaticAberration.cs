namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Chromatic Aberration</b>.
/// </summary>
public struct ColorChromaticAberration : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Offset</b>.
	/// </summary>
	[RDJsonProperty("Offset")]
	public float Offset { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_Chromatic_Aberration";
#else
	public static string Name => "CameraFilterPack_Color_Chromatic_Aberration";
#endif
}