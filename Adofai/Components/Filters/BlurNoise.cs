namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Noise</b>.
/// </summary>
public struct BlurNoise : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Level</b>.
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	public RDPointN Distance { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Noise";
#else
	public static string Name => "CameraFilterPack_Blur_Noise";
#endif
}