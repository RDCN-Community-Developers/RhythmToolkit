namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera SoftLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_SoftLight")]
[RDJsonObjectSerializable]
public struct BlendToCameraSoftLight : IFilter
{
	public FilterType Type => FilterType.BlendToCameraSoftLight;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}