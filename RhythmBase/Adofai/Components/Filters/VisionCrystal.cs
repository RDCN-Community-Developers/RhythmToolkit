namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Crystal</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Crystal")]
public struct VisionCrystal : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>X</b>.
	/// </summary>
	[RDJsonProperty("X")]
	public float X { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Y</b>.
	/// </summary>
	[RDJsonProperty("Y")]
	public float Y { get; set; }
}