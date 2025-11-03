namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus ThermaVision</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Oculus_ThermaVision")]
public struct OculusThermaVision : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Therma_Variation</b>.
	/// </summary>
	[RDJsonProperty("Therma_Variation")]
	public float ThermaVariation { get; set; }
}