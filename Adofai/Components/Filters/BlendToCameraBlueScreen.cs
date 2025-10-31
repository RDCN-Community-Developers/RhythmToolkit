namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera BlueScreen</b>.
/// </summary>
public struct BlendToCameraBlueScreen : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	public float BlendFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blend2Camera_BlueScreen";
#else
	public static string Name => "CameraFilterPack_Blend2Camera_BlueScreen";
#endif
}