namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>AAA WaterDrop</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_AAA_WaterDrop")]
public struct AaaWaterDrop : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>SizeX</b>.
	/// </summary>
	[RDJsonProperty("SizeX")]
	public float SizeX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>SizeY</b>.
	/// </summary>
	[RDJsonProperty("SizeY")]
	public float SizeY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
}