namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera PhotoshopFilters</b>.
/// </summary>
public struct BlendToCameraPhotoshopFilters : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	public float BlendFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blend2Camera_PhotoshopFilters";
#else
	public static string Name => "CameraFilterPack_Blend2Camera_PhotoshopFilters";
#endif
}