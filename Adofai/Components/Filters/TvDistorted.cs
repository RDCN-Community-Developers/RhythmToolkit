namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV Distorted</b>.
/// </summary>
public struct TvDistorted : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>RGB</b>.
	/// </summary>
	[RDJsonProperty("RGB")]
	public float Rgb { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_Distorted";
#else
	public static string Name => "CameraFilterPack_TV_Distorted";
#endif
}