namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Gradients FireGradient</b>.
/// </summary>
public struct GradientsFireGradient : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Switch</b>.
	/// </summary>
	public float Switch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Gradients_FireGradient";
#else
	public static string Name => "CameraFilterPack_Gradients_FireGradient";
#endif
}