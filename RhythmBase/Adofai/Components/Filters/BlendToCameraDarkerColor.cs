namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera DarkerColor</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_DarkerColor")]
[RDJsonObjectSerializable]
public struct BlendToCameraDarkerColor : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraDarkerColor;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}