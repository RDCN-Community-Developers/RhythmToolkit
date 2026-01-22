namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Luminosity</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Luminosity")]
public struct BlendToCameraLuminosity : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}