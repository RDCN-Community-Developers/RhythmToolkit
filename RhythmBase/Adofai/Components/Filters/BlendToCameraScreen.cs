namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera Screen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_Screen")]
[RDJsonObjectSerializable]
public struct BlendToCameraScreen : IFilter
{
	public FilterType Type => FilterType.BlendToCameraScreen;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}