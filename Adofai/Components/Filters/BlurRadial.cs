namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Radial</b>.
/// </summary>
public struct BlurRadial : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>MovX</b>.
	/// </summary>
	public float MovX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>MovY</b>.
	/// </summary>
	public float MovY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Radial";
#else
	public static string Name => "CameraFilterPack_Blur_Radial";
#endif
}