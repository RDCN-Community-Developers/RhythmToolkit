namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur GaussianBlur</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_GaussianBlur")]
[RDJsonObjectSerializable]
public struct BlurGaussianBlur : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlurGaussianBlur;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
}