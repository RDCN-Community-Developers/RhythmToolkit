namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera LinearDodge</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_LinearDodge")]
[RDJsonObjectSerializable]
public struct BlendToCameraLinearDodge : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraLinearDodge;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}