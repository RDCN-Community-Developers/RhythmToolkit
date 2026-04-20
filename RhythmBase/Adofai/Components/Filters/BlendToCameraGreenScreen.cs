namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera GreenScreen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_GreenScreen")]
[RDJsonObjectSerializable]
public struct BlendToCameraGreenScreen : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraGreenScreen;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}