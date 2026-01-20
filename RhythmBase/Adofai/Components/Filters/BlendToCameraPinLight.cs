namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera PinLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_PinLight")]
public struct BlendToCameraPinLight : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}