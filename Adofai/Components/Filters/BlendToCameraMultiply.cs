namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Multiply</b>.
/// </summary>
public struct BlendToCameraMultiply : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blend2Camera_Multiply";
#else
	public static string Name => "CameraFilterPack_Blend2Camera_Multiply";
#endif
}