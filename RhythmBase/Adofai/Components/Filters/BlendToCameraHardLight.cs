namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera HardLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_HardLight")]
[RDJsonObjectSerializable]
public struct BlendToCameraHardLight : IFilter
{
	public FilterType Type => FilterType.BlendToCameraHardLight;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}