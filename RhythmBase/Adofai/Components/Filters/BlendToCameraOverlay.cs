namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Overlay</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Overlay")]
[RDJsonObjectSerializable]
public struct BlendToCameraOverlay : IFilter
{
	public FilterType Type => FilterType.BlendToCameraOverlay;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}