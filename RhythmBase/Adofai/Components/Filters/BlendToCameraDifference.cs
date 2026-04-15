namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Difference</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Difference")]
[RDJsonObjectSerializable]
public struct BlendToCameraDifference : IFilter
{
	public FilterType Type => FilterType.BlendToCameraDifference;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}