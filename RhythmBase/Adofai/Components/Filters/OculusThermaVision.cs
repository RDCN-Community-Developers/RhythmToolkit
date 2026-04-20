namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus ThermaVision</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Oculus_ThermaVision")]
[RDJsonObjectSerializable]
public struct OculusThermaVision : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.OculusThermaVision;
	/// <summary>
	/// Gets or sets the value of the <b>Therma_Variation</b>.
	/// </summary>
	[RDJsonAlias("Therma_Variation")]
	public float ThermaVariation { get; set; }
}