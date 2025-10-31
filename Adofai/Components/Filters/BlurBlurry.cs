namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Blurry</b>.
/// </summary>
public struct BlurBlurry : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	public int FastFilter { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Blurry";
#else
	public static string Name => "CameraFilterPack_Blur_Blurry";
#endif
}