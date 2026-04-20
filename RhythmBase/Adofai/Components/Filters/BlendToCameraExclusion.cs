namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Exclusion</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Exclusion")]
[RDJsonObjectSerializable]
public struct BlendToCameraExclusion : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraExclusion;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}