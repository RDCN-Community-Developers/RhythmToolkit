namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Hue</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Hue")]
[RDJsonObjectSerializable]
public struct BlendToCameraHue : IFilter
{
	public FilterType Type => FilterType.BlendToCameraHue;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}