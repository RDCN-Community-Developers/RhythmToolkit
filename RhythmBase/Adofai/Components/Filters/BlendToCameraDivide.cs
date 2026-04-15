namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Divide</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Divide")]
[RDJsonObjectSerializable]
public struct BlendToCameraDivide : IFilter
{
	public FilterType Type => FilterType.BlendToCameraDivide;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}