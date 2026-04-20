namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion BlackHole</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_BlackHole")]
[RDJsonObjectSerializable]
public struct DistortionBlackHole : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DistortionBlackHole;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}