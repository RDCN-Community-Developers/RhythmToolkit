namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Saturation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Saturation")]
[RDJsonObjectSerializable]
public struct BlendToCameraSaturation : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraSaturation;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}