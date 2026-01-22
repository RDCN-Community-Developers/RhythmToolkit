namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Water Drop</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_Water_Drop")]
public struct DistortionWaterDrop : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>WaveIntensity</b>.
	/// </summary>
	[RDJsonAlias("WaveIntensity")]
	public float WaveIntensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>NumberOfWaves</b>.
	/// </summary>
	[RDJsonAlias("NumberOfWaves")]
	public int NumberOfWaves { get; set; }
}