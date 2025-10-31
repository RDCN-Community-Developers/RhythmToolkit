namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Distortion Dissipation</b>.
/// </summary>
public struct DistortionDissipation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Dissipation</b>.
	/// </summary>
	public float Dissipation { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Distortion_Dissipation";
#else
	public static string Name => "CameraFilterPack_Distortion_Dissipation";
#endif
}