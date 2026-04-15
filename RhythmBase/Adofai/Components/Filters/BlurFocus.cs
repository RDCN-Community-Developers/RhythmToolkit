namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Focus</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Focus")]
[RDJsonObjectSerializable]
public struct BlurFocus : IFilter
{
	public FilterType Type => FilterType.BlurFocus;
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonAlias("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Eyes</b>.
	/// </summary>
	[RDJsonAlias("_Eyes")]
	public float Eyes { get; set; }
}