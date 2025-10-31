namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation OilPaint</b>.
/// </summary>
public struct PixelisationOilPaint : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Pixelisation_OilPaint";
#else
	public static string Name => "CameraFilterPack_Pixelisation_OilPaint";
#endif
}