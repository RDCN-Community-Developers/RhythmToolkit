namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus ThermaVision</b>.
/// </summary>
public struct OculusThermaVision : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Therma_Variation</b>.
	/// </summary>
	[RDJsonProperty("Therma_Variation")]
	public float ThermaVariation { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Oculus_ThermaVision";
#else
	public static string Name => "CameraFilterPack_Oculus_ThermaVision";
#endif
}