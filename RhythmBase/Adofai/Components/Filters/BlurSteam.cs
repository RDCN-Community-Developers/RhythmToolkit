namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Steam</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Steam")]
public struct BlurSteam : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonProperty("Radius")]
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Quality</b>.
	/// </summary>
	[RDJsonProperty("Quality")]
	public float Quality { get; set; }
}