namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Drost</b>.
/// </summary>
public struct VisionDrost : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Drost";
#else
	public static string Name => "CameraFilterPack_Vision_Drost";
#endif
}