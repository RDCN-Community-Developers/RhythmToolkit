namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera VividLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_VividLight")]
[RDJsonObjectSerializable]
public struct BlendToCameraVividLight : IFilter
{
	public FilterType Type => FilterType.BlendToCameraVividLight;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}