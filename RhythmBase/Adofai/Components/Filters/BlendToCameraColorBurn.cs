namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera ColorBurn</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_ColorBurn")]
[RDJsonObjectSerializable]
public struct BlendToCameraColorBurn : IFilter
{
	public FilterType Type => FilterType.BlendToCameraColorBurn;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}