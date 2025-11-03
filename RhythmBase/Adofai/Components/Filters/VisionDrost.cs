namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Drost</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Drost")]
public struct VisionDrost : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
}