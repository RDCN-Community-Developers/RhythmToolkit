namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera ColorDodge</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_ColorDodge")]
[RDJsonObjectSerializable]
public struct BlendToCameraColorDodge : IFilter
{
	public FilterType Type => FilterType.BlendToCameraColorDodge;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}