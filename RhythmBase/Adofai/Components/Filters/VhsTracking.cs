namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>VHS Tracking</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_VHS_Tracking")]
public struct VhsTracking : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Tracking</b>.
	/// </summary>
	[RDJsonProperty("Tracking")]
	public float Tracking { get; set; }
}