namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX 8bits</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_8bits")]
[RDJsonObjectSerializable]
public struct Fx8Bits : IFilter
{
	public FilterType Type => FilterType.Fx8Bits;
	/// <summary>
	/// Gets or sets the value of the <b>ResolutionX</b>.
	/// </summary>
	[RDJsonAlias("ResolutionX")]
	public int ResolutionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ResolutionY</b>.
	/// </summary>
	[RDJsonAlias("ResolutionY")]
	public int ResolutionY { get; set; }
}