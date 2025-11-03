namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Subtract</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Subtract")]
public struct BlendToCameraSubtract : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}