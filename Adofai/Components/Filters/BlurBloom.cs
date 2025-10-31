namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Bloom</b>.
/// </summary>
public struct BlurBloom : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonProperty("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Glow</b>.
	/// </summary>
	[RDJsonProperty("Glow")]
	public float Glow { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Bloom";
#else
	public static string Name => "CameraFilterPack_Blur_Bloom";
#endif
}