namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Blend</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Blend")]
[RDJsonObjectSerializable]
public struct BlendToCameraBlend : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraBlend;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}