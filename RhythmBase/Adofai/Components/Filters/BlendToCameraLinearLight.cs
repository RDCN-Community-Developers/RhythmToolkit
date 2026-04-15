namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera LinearLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_LinearLight")]
[RDJsonObjectSerializable]
public struct BlendToCameraLinearLight : IFilter
{
	public FilterType Type => FilterType.BlendToCameraLinearLight;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}