namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Divide</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Divide")]
public struct BlendToCameraDivide : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}