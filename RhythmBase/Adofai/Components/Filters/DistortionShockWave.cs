namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion ShockWave</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Distortion_ShockWave")]
public struct DistortionShockWave : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonProperty("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonProperty("PosY")]
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
}