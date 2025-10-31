namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation OilPaintHQ</b>.
/// </summary>
public struct PixelisationOilPaintHQ : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Pixelisation_OilPaintHQ";
#else
	public static string Name => "CameraFilterPack_Pixelisation_OilPaintHQ";
#endif
}