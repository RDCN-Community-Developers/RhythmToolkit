namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>NightVision 4</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_NightVision_4")]
public struct NightVision4 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonProperty("FadeFX")]
	public float FadeFX { get; set; }
}