namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Oculus_NightVision2")]
public struct OculusNightVisionTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonProperty("FadeFX")]
	public float FadeFX { get; set; }
}