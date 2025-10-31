namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Oculus NightVision2</b>.
/// </summary>
public struct OculusNightVisionTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	public float FadeFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Oculus_NightVision2";
#else
	public static string Name => "CameraFilterPack_Oculus_NightVision2";
#endif
}