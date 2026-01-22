namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision3</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Oculus_NightVision3")]
public struct OculusNightVision3 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Greenness</b>.
	/// </summary>
	[RDJsonAlias("Greenness")]
	public float Greenness { get; set; }
}