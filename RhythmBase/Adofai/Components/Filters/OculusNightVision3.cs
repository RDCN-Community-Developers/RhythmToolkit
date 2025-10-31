namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision3</b>.
/// </summary>
public struct OculusNightVision3 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Greenness</b>.
	/// </summary>
	[RDJsonProperty("Greenness")]
	public float Greenness { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Oculus_NightVision3";
#else
	public static string Name => "CameraFilterPack_Oculus_NightVision3";
#endif
}