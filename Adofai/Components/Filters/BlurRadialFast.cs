namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Radial Fast</b>.
/// </summary>
public struct BlurRadialFast : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>MovX</b>.
	/// </summary>
	[RDJsonProperty("MovX")]
	public float MovX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>MovY</b>.
	/// </summary>
	[RDJsonProperty("MovY")]
	public float MovY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Radial_Fast";
#else
	public static string Name => "CameraFilterPack_Blur_Radial_Fast";
#endif
}