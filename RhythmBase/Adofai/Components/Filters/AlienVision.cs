namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Alien Vision</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Alien_Vision")]
public struct AlienVision : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Therma_Variation</b>.
	/// </summary>
	[RDJsonAlias("Therma_Variation")]
	public float ThermaVariation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}