namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision5</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Oculus_NightVision5")]
public struct OculusNightVision5 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonAlias("FadeFX")]
	public float FadeFX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonAlias("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Smooth</b>.
	/// </summary>
	[RDJsonAlias("_Smooth")]
	public float Smooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Dist</b>.
	/// </summary>
	[RDJsonAlias("_Dist")]
	public float Dist { get; set; }
}