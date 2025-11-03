namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Overlay</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Overlay")]
public struct BlendToCameraOverlay : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}