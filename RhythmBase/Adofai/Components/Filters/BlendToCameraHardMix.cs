namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera HardMix</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_HardMix")]
[RDJsonObjectSerializable]
public struct BlendToCameraHardMix : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlendToCameraHardMix;
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}