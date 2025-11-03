namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera LinearBurn</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_LinearBurn")]
public struct BlendToCameraLinearBurn : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}