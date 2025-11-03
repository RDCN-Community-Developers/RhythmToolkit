namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Wave Horizontal</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Wave_Horizontal")]
public struct DistortionWaveHorizontal : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>WaveIntensity</b>.
	/// </summary>
	[RDJsonProperty("WaveIntensity")]
	public float WaveIntensity { get; set; }
}