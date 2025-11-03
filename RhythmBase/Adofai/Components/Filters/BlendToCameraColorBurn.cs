namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera ColorBurn</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_ColorBurn")]
public struct BlendToCameraColorBurn : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}