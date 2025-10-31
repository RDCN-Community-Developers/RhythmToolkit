namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Adjust ColorRGB</b>.
/// </summary>
public struct ColorsAdjustColorRGB : IFilter
{
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_Adjust_ColorRGB";
#else
	public static string Name => "CameraFilterPack_Colors_Adjust_ColorRGB";
#endif
}