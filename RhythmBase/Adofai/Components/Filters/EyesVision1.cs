namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>EyesVision 1</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_EyesVision_1")]
public struct EyesVision1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_EyeWave</b>.
	/// </summary>
	[RDJsonProperty("_EyeWave")]
	public float EyeWave { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_EyeSpeed</b>.
	/// </summary>
	[RDJsonProperty("_EyeSpeed")]
	public float EyeSpeed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_EyeMove</b>.
	/// </summary>
	[RDJsonProperty("_EyeMove")]
	public float EyeMove { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_EyeBlink</b>.
	/// </summary>
	[RDJsonProperty("_EyeBlink")]
	public float EyeBlink { get; set; }
}