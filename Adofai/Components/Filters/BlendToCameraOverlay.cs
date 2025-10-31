namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Overlay</b>.
/// </summary>
public struct BlendToCameraOverlay : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	public float BlendFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blend2Camera_Overlay";
#else
	public static string Name => "CameraFilterPack_Blend2Camera_Overlay";
#endif
}