namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Color</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Color")]
public struct BlendToCameraColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}