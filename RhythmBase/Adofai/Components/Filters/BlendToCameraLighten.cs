namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Lighten</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Lighten")]
[RDJsonObjectSerializable]
public struct BlendToCameraLighten : IFilter
{
	public FilterType Type => FilterType.BlendToCameraLighten;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}