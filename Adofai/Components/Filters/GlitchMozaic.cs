namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Glitch Mozaic</b>.
/// </summary>
public struct GlitchMozaic : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Glitch_Mozaic";
#else
	public static string Name => "CameraFilterPack_Glitch_Mozaic";
#endif
}