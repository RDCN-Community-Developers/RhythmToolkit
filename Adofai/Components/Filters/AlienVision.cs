namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Alien Vision</b>.
/// </summary>
public struct AlienVision : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Therma_Variation</b>.
	/// </summary>
	[RDJsonProperty("Therma_Variation")]
	public float ThermaVariation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Alien_Vision";
#else
	public static string Name => "CameraFilterPack_Alien_Vision";
#endif
}