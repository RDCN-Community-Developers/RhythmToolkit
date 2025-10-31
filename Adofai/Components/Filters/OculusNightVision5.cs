namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision5</b>.
/// </summary>
public struct OculusNightVision5 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonProperty("FadeFX")]
	public float FadeFX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonProperty("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Smooth</b>.
	/// </summary>
	[RDJsonProperty("_Smooth")]
	public float Smooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Dist</b>.
	/// </summary>
	[RDJsonProperty("_Dist")]
	public float Dist { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Oculus_NightVision5";
#else
	public static string Name => "CameraFilterPack_Oculus_NightVision5";
#endif
}