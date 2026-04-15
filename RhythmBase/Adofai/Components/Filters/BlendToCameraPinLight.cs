namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera PinLight</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_PinLight")]
[RDJsonObjectSerializable]
public struct BlendToCameraPinLight : IFilter
{
	public FilterType Type => FilterType.BlendToCameraPinLight;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}