namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion ShockWave</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_ShockWave")]
[RDJsonObjectSerializable]
public struct DistortionShockWave : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DistortionShockWave;
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonAlias("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonAlias("PosY")]
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}