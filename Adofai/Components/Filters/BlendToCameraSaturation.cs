namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Saturation</b>.
/// </summary>
public struct BlendToCameraSaturation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blend2Camera_Saturation";
#else
	public static string Name => "CameraFilterPack_Blend2Camera_Saturation";
#endif
}