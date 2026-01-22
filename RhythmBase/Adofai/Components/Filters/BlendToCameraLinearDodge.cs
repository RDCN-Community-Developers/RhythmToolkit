namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera LinearDodge</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_LinearDodge")]
public struct BlendToCameraLinearDodge : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}