namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Wave Horizontal</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Wave_Horizontal")]
[RDJsonObjectSerializable]
public struct DistortionWaveHorizontal : IFilter
{
	public FilterType Type => FilterType.DistortionWaveHorizontal;
	/// <summary>
	/// Gets or sets the value of the <b>WaveIntensity</b>.
	/// </summary>
	[RDJsonAlias("WaveIntensity")]
	public float WaveIntensity { get; set; }
}