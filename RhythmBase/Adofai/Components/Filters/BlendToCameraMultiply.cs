namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Multiply</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Multiply")]
[RDJsonObjectSerializable]
public struct BlendToCameraMultiply : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraMultiply;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}