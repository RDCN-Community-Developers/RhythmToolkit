namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera LinearBurn</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_LinearBurn")]
[RDJsonObjectSerializable]
public struct BlendToCameraLinearBurn : IFilter
{
	public FilterType Type => FilterType.BlendToCameraLinearBurn;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}