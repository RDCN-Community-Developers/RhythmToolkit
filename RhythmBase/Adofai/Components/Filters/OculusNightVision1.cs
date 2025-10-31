namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision1</b>.
/// </summary>
public struct OculusNightVision1 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Vignette</b>.
	/// </summary>
	[RDJsonProperty("Vignette")]
	public float Vignette { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Linecount</b>.
	/// </summary>
	[RDJsonProperty("Linecount")]
	public float Linecount { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Oculus_NightVision1";
#else
	public static string Name => "CameraFilterPack_Oculus_NightVision1";
#endif
}