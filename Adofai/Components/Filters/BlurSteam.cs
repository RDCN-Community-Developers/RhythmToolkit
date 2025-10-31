namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Steam</b>.
/// </summary>
public struct BlurSteam : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	public float Radius { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Quality</b>.
	/// </summary>
	public float Quality { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Steam";
#else
	public static string Name => "CameraFilterPack_Blur_Steam";
#endif
}