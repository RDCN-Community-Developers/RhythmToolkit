namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Noise</b>.
/// </summary>
public struct ColorNoise : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Noise</b>.
	/// </summary>
	[RDJsonProperty("Noise")]
	public float Noise { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_Noise";
#else
	public static string Name => "CameraFilterPack_Color_Noise";
#endif
}