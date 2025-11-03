namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera SoftLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_SoftLight")]
public struct BlendToCameraSoftLight : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}