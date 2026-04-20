namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Subtract</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Subtract")]
[RDJsonObjectSerializable]
public struct BlendToCameraSubtract : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraSubtract;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}