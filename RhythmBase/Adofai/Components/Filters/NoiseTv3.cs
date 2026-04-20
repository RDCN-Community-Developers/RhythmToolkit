namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Noise TV 3</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Noise_TV_3")]
[RDJsonObjectSerializable]
public struct NoiseTv3 : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.NoiseTv3;
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonAlias("Fade")]
	public float Fade { get; set; }
}