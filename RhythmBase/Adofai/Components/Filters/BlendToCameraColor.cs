namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Color</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Color")]
[RDJsonObjectSerializable]
public struct BlendToCameraColor : IFilter
{
	public FilterType Type => FilterType.BlendToCameraColor;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}